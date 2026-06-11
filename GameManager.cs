using UnityEngine;
using TMPro;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score = 0;
    public TextMeshProUGUI scoreText;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // 🔥 bikin timing build lebih stabil
            Application.targetFrameRate = 60;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        LoadCheckpointData();
    }

    // 🔥 LISTENER UNTUK SCENE LOAD
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(RefreshTilemaps());
    }

    // 🔥 FIX TILEMAP BUILD BUG
    IEnumerator RefreshTilemaps()
    {
        // tunggu beberapa frame biar semua siap
        yield return null;
        yield return null;

        Tilemap[] tilemaps = FindObjectsOfType<Tilemap>();

        foreach (Tilemap tm in tilemaps)
        {
            tm.RefreshAllTiles();
        }

        Debug.Log("Tilemap refreshed dari GameManager");
    }

    // ======================
    // SCORE SYSTEM
    // ======================

    public void AddScore(int value)
    {
        score += value;
        SaveCoin();
        UpdateUI();
    }

    public void SaveCoin()
    {
        PlayerPrefs.SetInt("SavedCoin", score);
    }

    public void LoadCheckpointData()
    {
        score = PlayerPrefs.GetInt("SavedCoin", 0);
        UpdateUI();
    }

    public void SetScoreText(TextMeshProUGUI text)
    {
        scoreText = text;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Coins : " + score;
        }
    }

    // ======================
    // RESET GAME
    // ======================

    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
        score = 0;
        UpdateUI();

        Debug.Log("Game Reset - Semua data dihapus");
    }
}