using UnityEngine;

public class Coin : MonoBehaviour
{
    public string coinID;

    private bool taken = false;
    private Collider2D col;

    void Start()
    {
        col = GetComponent<Collider2D>();

        if (PlayerPrefs.GetInt(coinID, 0) == 1)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (taken) return;

        if (!collision.CompareTag("Player")) return;

        taken = true;

        col.enabled = false;

        PlayerPrefs.SetInt(coinID, 1);
        PlayerPrefs.Save();

        GameManager.instance.AddScore(1);

        Destroy(gameObject);
    }
}