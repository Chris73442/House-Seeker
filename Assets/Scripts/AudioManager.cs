using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource bgmSource;
    public AudioClip mainMenuMusic;
    public AudioClip gameplayMusic;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        PlayMainMenu();
    }

    // =========================
    // 🎵 BASIC PLAY FUNCTIONS
    // =========================

    public void PlayMainMenu()
    {
        Debug.Log("Play Main Menu Music");

        if (bgmSource == null)
        {
            Debug.LogError("AudioSource belum di-assign!");
            return;
        }

        if (mainMenuMusic == null)
        {
            Debug.LogError("MainMenuMusic belum di-assign!");
            return;
        }

        bgmSource.clip = mainMenuMusic;
        bgmSource.Play();
    }

    public void PlayGameplay()
    {
        Debug.Log("Play Gameplay Music");

        if (bgmSource == null || gameplayMusic == null) return;

        bgmSource.clip = gameplayMusic;
        bgmSource.Play();
    }

    public void StopMusic()
    {
        bgmSource.Stop();
    }

    // =========================
    // 🎯 SCENE LOGIC
    // =========================

    public void OnEnterMainMenu()
    {
        PlayMainMenu();
    }

    public void OnPlayToGuide()
    {
        // ❌ tidak reset → biarkan lanjut
    }

    public void OnGuideToGameplay()
    {
        PlayGameplay();
    }

    public void OnContinueGame()
    {
        PlayGameplay();
    }

    public void OnFailedToMenu()
    {
        PlayMainMenu();
    }

    public void OnCreditsToMenu()
    {
        PlayMainMenu();
    }
}