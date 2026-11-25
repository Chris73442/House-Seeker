using UnityEngine;
using UnityEngine.SceneManagement;

public class FailedScene : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) ||
            Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            RetryGame();
        }
    }

    void RetryGame()
    {
        SceneManager.LoadScene("GameplayScene");
    }
}