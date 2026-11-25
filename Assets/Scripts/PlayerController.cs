using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Jump")]
    public float jumpForce = 10f;
    public float gravityScale = 3f;

    [Header("Jump Limit")]
    public int maxJump = 3;
    private int jumpLeft;

    [Header("Ground Check Layer")]
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator animator;

    private float moveInput;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        rb.gravityScale = gravityScale;
        jumpLeft = maxJump;

        // LOAD COIN
        if (GameManager.instance != null)
        {
            GameManager.instance.LoadCheckpointData();
        }

        // LOAD CHECKPOINT POSITION
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

        // FLIP CHARACTER
        if (moveInput > 0)
            sr.flipX = false;
        else if (moveInput < 0)
            sr.flipX = true;

        // JUMP
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpLeft > 0)
            {
                rb.velocity = new Vector2(
                    rb.velocity.x,
                    jumpForce
                );

                jumpLeft--;
                isGrounded = false;
            }
        }

        // UPDATE ANIMATION
        animator.SetFloat("Speed", Mathf.Abs(moveInput));
        animator.SetBool("IsGrounded", isGrounded);

        // DIE
        if (transform.position.y < -10f)
        {
            Die();
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(
            moveInput * moveSpeed,
            rb.velocity.y
        );
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

    void Die()
    {
        // RESET KOIN YANG BELUM TERSIMPAN
        if (GameManager.instance != null)
        {
            GameManager.instance.RestoreTemporaryCoins();
        }

        // MASUK KE FAILED SCENE
        SceneManager.LoadScene("Failed Scene");
    }
}
