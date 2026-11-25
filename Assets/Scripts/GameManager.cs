using UnityEngine;
using TMPro;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Score")]
    public int score = 0;
    public TextMeshProUGUI scoreText;

    private int savedScore = 0;

    // Koin yang sudah diambil tetapi belum mencapai checkpoint
    private List<Coin> temporaryCoins = new List<Coin>();

    // Penanda pemulihan koin setelah GameplayScene dimuat ulang
    private bool restoreCoinsOnGameplayLoad = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

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

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // =========================
    // SCENE LOADED
    // =========================

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(RefreshTilemaps());

        if (scene.name == "GameplayScene" && restoreCoinsOnGameplayLoad)
        {
            StartCoroutine(RestoreCoinsAfterSceneLoad());
        }
    }

    IEnumerator RefreshTilemaps()
    {
        yield return null;
        yield return null;

        Tilemap[] tilemaps = FindObjectsOfType<Tilemap>();

        foreach (Tilemap tm in tilemaps)
        {
            tm.RefreshAllTiles();
        }

        Debug.Log("Tilemap refreshed dari GameManager");
    }

    // =========================
    // SCORE SYSTEM
    // =========================

    public void AddScore(int value)
    {
        score += value;
        UpdateUI();
    }

    public void RegisterTemporaryCoin(Coin coin)
    {
        if (coin != null && !temporaryCoins.Contains(coin))
        {
            temporaryCoins.Add(coin);
        }
    }

    // =========================
    // CHECKPOINT SYSTEM
    // =========================

    public void SaveCoin()
    {
        savedScore = score;

        foreach (Coin coin in temporaryCoins)
        {
            if (coin != null && !string.IsNullOrEmpty(coin.coinID))
            {
                PlayerPrefs.SetInt(coin.coinID, 1);
            }
        }

        PlayerPrefs.SetInt("SavedCoin", savedScore);
        PlayerPrefs.Save();

        temporaryCoins.Clear();

        Debug.Log("Coin saved at checkpoint: " + savedScore);
    }

    // =========================
    // RESTORE COIN SYSTEM
    // =========================

    public void RestoreTemporaryCoins()
    {
        // Kembalikan score ke score checkpoint terakhir
        score = savedScore;

        // Tandai agar koin dipulihkan ketika GameplayScene dibuka
        restoreCoinsOnGameplayLoad = true;

        // Bersihkan daftar koin sementara
        temporaryCoins.Clear();

        UpdateUI();

        Debug.Log("Temporary coins restored: " + score);
    }

    IEnumerator RestoreCoinsAfterSceneLoad()
    {
        // Tunggu objek Coin selesai menjalankan Start()
        yield return null;
        yield return null;

        Coin[] allCoins = FindObjectsOfType<Coin>(true);

        foreach (Coin coin in allCoins)
        {
            if (coin == null)
                continue;

            // Koin yang belum tersimpan akan diaktifkan kembali
            if (PlayerPrefs.GetInt(coin.coinID, 0) == 0)
            {
                coin.RestoreCoin();
            }
        }

        restoreCoinsOnGameplayLoad = false;

        Debug.Log("Koin sementara berhasil dipulihkan.");
    }

    // =========================
    // LOAD CHECKPOINT DATA
    // =========================

    public void LoadCheckpointData()
    {
        savedScore = PlayerPrefs.GetInt("SavedCoin", 0);
        score = savedScore;

        UpdateUI();
    }

    // =========================
    // UI SYSTEM
    // =========================

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

    // =========================
    // RESET GAME
    // =========================

    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();

        score = 0;
        savedScore = 0;

        temporaryCoins.Clear();
        restoreCoinsOnGameplayLoad = false;

        UpdateUI();

        Debug.Log("Game Reset - Semua data dihapus");
    }
}
