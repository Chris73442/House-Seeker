using UnityEngine;
using UnityEngine.SceneManagement;

public class FailedScene : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            RetryGame();
        }
    }

    void RetryGame()
    {
        int hasCheckpoint = PlayerPrefs.GetInt("HasCheckpoint", 0);

        if (hasCheckpoint == 1)
        {
            // 👉 lanjut dari checkpoint
            SceneManager.LoadScene("GameplayScene");
        }
        else
        {
            // 👉 ulang dari awal
            SceneManager.LoadScene("GameplayScene"); 
            PlayerPrefs.DeleteKey("CP_X");
            PlayerPrefs.DeleteKey("CP_Y");
            PlayerPrefs.DeleteKey("CP_Z");
        }
    }
}