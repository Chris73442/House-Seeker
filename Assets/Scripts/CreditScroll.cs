
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CreditScroll : MonoBehaviour
{
    [Header("UI")]
    public RectTransform creditText;
    public RectTransform viewArea;
    public GameObject pressEnterText;

    [Header("Scroll Settings")]
    public float scrollSpeed = 100f;

    [Header("Scene")]
    public string mainMenuSceneName = "Main Menu Scene";

    private TextMeshProUGUI tmp;

    private float stopY;
    private bool finished = false;

    void Start()
    {
        tmp = creditText.GetComponent<TextMeshProUGUI>();
        tmp.ForceMeshUpdate();

        // Center alignment
        tmp.alignment = TextAlignmentOptions.Center;

        // Center horizontal position
        Vector2 position = creditText.anchoredPosition;
        position.x = -280f;
        creditText.anchoredPosition = position;

        float textHeight = tmp.preferredHeight;
        float viewHeight = viewArea.rect.height;

        // Mulai dari bawah layar
        float startY = -(textHeight / 2f) - (viewHeight / 2f);
        creditText.anchoredPosition = new Vector2(-280, startY);

        // Posisi berhenti setelah seluruh credit selesai
        stopY = startY + textHeight + viewHeight;

        // Sembunyikan Press Enter terlebih dahulu
        if (pressEnterText != null)
            pressEnterText.SetActive(false);
    }

    void Update()
    {
        if (!finished)
        {
            // Scroll credit ke atas
            creditText.anchoredPosition +=
                Vector2.up * scrollSpeed * Time.deltaTime;

            // Tunggu sampai seluruh credit selesai
            if (creditText.anchoredPosition.y >= stopY)
            {
                creditText.anchoredPosition =
                new Vector2(-280, stopY);

                finished = true;

                // Hilangkan seluruh tulisan credit
                creditText.gameObject.SetActive(false);

                // Tampilkan Press Enter
                if (pressEnterText != null)
                {
                    pressEnterText.SetActive(true);
                }
            }
        }
        else
        {
            // Kembali ke Main Menu saat Enter ditekan
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(mainMenuSceneName);
            }
        }
    }
}
