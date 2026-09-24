using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool isActivated = false;
    private Collider2D col;
    private SpriteRenderer sr;

    void Awake()
    {
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();

        isActivated = false;
    }

    void Start()
    {
        if (PlayerPrefs.GetInt("CheckpointUsed", 0) == 1)
        {
            SetInactive();
            isActivated = true;
        }
        else
        {
            SetActive();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (isActivated) return;

        isActivated = true;

        PlayerPrefs.SetInt("CheckpointUsed", 1);
        PlayerPrefs.SetFloat("CheckpointX", transform.position.x);
        PlayerPrefs.SetFloat("CheckpointY", transform.position.y);
        PlayerPrefs.SetInt("SavedCoin", GameManager.instance.score);
        PlayerPrefs.SetInt("HasSave", 1);
        PlayerPrefs.Save();

        SetInactive();
    }

    void SetInactive()
    {
        if (col != null) col.enabled = false;
        if (sr != null) sr.enabled = false;
    }

    void SetActive()
    {
        if (col != null) col.enabled = true;
        if (sr != null) sr.enabled = true;
    }
}