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
    public float scrollSpeed = 80f;

    [Header("Press Enter Timing")]
    public float showOffset = 200f; // muncul lebih awal (atur di sini)

    [Header("Scene")]
    public string mainMenuSceneName = "Main Menu Scene";

    private TextMeshProUGUI tmp;

    private float stopY;
    private bool finished = false;

    void Start()
    {
        tmp = creditText.GetComponent<TextMeshProUGUI>();
        tmp.ForceMeshUpdate();

        float textHeight = tmp.preferredHeight;
        float viewHeight = viewArea.rect.height;

        // 🔥 mulai dari bawah layar (no delay)
        float startY = -(textHeight / 2f) - (viewHeight / 2f);
        creditText.anchoredPosition = new Vector2(0, startY);

        // 🔥 stop di akhir credit
        stopY = startY + textHeight + viewHeight;

        // sembunyikan dulu
        if (pressEnterText != null)
            pressEnterText.SetActive(false);
    }

    void Update()
    {
        if (!finished)
        {
            creditText.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

            // 🔥 munculin lebih awal (biar nggak delay)
            if (pressEnterText != null &&
                !pressEnterText.activeSelf &&
                creditText.anchoredPosition.y >= stopY - showOffset)
            {
                pressEnterText.SetActive(true);
            }

            // stop di akhir
            if (creditText.anchoredPosition.y >= stopY)
            {
                creditText.anchoredPosition = new Vector2(0, stopY);
                finished = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(mainMenuSceneName);
            }
        }
    }
}