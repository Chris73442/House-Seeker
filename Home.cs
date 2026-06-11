using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Home : MonoBehaviour
{
    public GameObject buyText; // UI text
    public int requiredCoins = 9;

    private bool isNear = false;

    void Update()
    {
        if (!isNear) return;

        int coins = PlayerPrefs.GetInt("SavedCoin", 0);

        // tampilkan text kalau dekat (aman dari null)
        if (buyText != null && !buyText.activeSelf)
        {
            buyText.SetActive(true);
        }

        // tekan F
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (coins == requiredCoins)
            {
                SceneManager.LoadScene("CreditScene");
            }
            else
            {
                // tidak cukup → tidak terjadi apa-apa
                Debug.Log("Coin tidak cukup");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = true;
            buyText.SetActive(true);
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