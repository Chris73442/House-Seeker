using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScene : MonoBehaviour
{
    void Start()
    {
        AudioManager.instance.OnEnterMainMenu();
    }

    public void PlayGame()
    {
        // 🔥 RESET SEMUA DATA (INI KUNCI UTAMA)
        PlayerPrefs.DeleteAll();
        PlayerPrefs.DeleteKey("CheckpointUsed");
        PlayerPrefs.DeleteKey("CheckpointX");
        PlayerPrefs.DeleteKey("CheckpointY");
        PlayerPrefs.DeleteKey("HasSave");

        AudioManager.instance.OnPlayToGuide();
        SceneManager.LoadScene("GuideScene");
    }

    public void OpenCredits()
    {
        SceneManager.LoadScene("CreditScene");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}