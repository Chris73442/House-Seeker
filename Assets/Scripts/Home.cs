
using UnityEngine;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour
{
    [Header("House Settings")]
    public GameObject buyText;
    public int requiredCoins = 9;

    private bool isNear = false;

    void Update()
    {
        if (!isNear) return;

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (GameManager.instance == null)
            {
                Debug.LogError("GameManager tidak ditemukan!");
                return;
            }

            // Ambil jumlah koin saat ini
            int coins = GameManager.instance.score;

            Debug.Log("Koin saat ini: " + coins);

            // CEK KOIN
            if (coins >= requiredCoins)
            {
                Debug.Log("Pembelian berhasil!");

                SceneManager.LoadScene("CreditScene");
            }
            else
            {
                Debug.Log("Koin tidak cukup! Butuh: " + requiredCoins);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = true;

            if (buyText != null)
            {
                buyText.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = false;

            if (buyText != null)
            {
                buyText.SetActive(false);
            }
        }
    }
}
