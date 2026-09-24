using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Jump")]
    public float jumpForce;
    public float gravityScale = 3f;

    [Header("Jump Limit")]
    public int maxJump = 3;
    private int jumpLeft;

    [Header("Ground Check Layer")]
    public LayerMask groundLayer;

    [Header("Sprites")]
    public Sprite idleSprite;
    public Sprite runSprite;
    public Sprite jumpSprite;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private float moveInput;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        rb.gravityScale = gravityScale;

        sr.sprite = idleSprite;

        jumpLeft = maxJump;

        // 🔥 LOAD COIN
        if (GameManager.instance != null)
        {
            GameManager.instance.LoadCheckpointData();
        }

        // 🔥 FIX UTAMA: hanya spawn checkpoint kalau benar-benar ada save
        if (PlayerPrefs.GetInt("HasSave", 0) == 1)
        {
            float x = PlayerPrefs.GetFloat("CheckpointX");
            float y = PlayerPrefs.GetFloat("CheckpointY");
            transform.position = new Vector2(x, y);
        }
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if (moveInput > 0)
            sr.flipX = false;
        else if (moveInput < 0)
            sr.flipX = true;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpLeft > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpLeft--;
            }
        }

        UpdateSprite();

        if (transform.position.y < -10f)
        {
            Die();
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = true;
            jumpLeft = maxJump;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = false;
        }
    }

    void UpdateSprite()
    {
        bool moving = Mathf.Abs(rb.velocity.x) > 0.1f;
        bool inAir = !isGrounded;

        if (inAir)
            sr.sprite = jumpSprite;
        else if (moving)
            sr.sprite = runSprite;
        else
            sr.sprite = idleSprite;
    }

    void Die()
    {
        SceneManager.LoadScene("Failed Scene");
    }
}