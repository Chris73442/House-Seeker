using UnityEngine;

public class Coin : MonoBehaviour
{
    public string coinID;

    private bool taken = false;

    void Start()
    {
        // Hanya hancurkan koin yang sudah tersimpan
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

        // Catat sebagai koin sementara
        GameManager.instance.RegisterTemporaryCoin(this);

        // Tambahkan score
        GameManager.instance.AddScore(1);

        // Sembunyikan koin, jangan dihancurkan
        gameObject.SetActive(false);
    }

    // Munculkan kembali koin saat respawn
    public void RestoreCoin()
    {
        taken = false;
        gameObject.SetActive(true);
    }
}
