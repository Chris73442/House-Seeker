using UnityEngine;
using UnityEngine.SceneManagement;

public class GuideController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            StartGame();
        }
    }

    void StartGame()
    {
        // 🔥 AMAN DARI ERROR
        if (AudioManager.instance != null)
        {
            AudioManager.instance.OnGuideToGameplay();
        }

        SceneManager.LoadScene("GameplayScene");
    }
}