// GameManager의 역할
// 1. 게임의 흐름 제어 (일시 정지, 진행)
// 2. 씬 관리
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Ready,
        Playing,
        Paused,
        GameOver,
        CutScene
    }

    public static GameManager Instance;

    private GameState gameState;
    private float playTime;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        InitGame();
    }

    public void InitGame()
    {
        gameState = GameState.Ready;
        playTime = 0f;
        Time.timeScale = 1f;

        UIManager.Instance.OnMainMenuUI();
    }

    public void GameStart()
    {
        gameState = GameState.Playing;

        UIManager.Instance.OffMainMenuUI();
    }

    public void GamePause()
    {
        gameState = GameState.Paused;
        Time.timeScale = 0f;

        UIManager.Instance.OnPauseUI();
    }

    public void GameResume()
    {
        gameState = GameState.Playing;
        Time.timeScale = 1f;

        UIManager.Instance.OffPauseUI();
    }

    public void GameOver()
    {
        gameState = GameState.GameOver;

        // SceneManager.LoadScene("GameOverScene");
    }

    public void GameRestart()
    {
        // SceneManager.LoadScene("Stage1");

        InitGame();
    }

    public void PlayCutScene()
    {
        gameState = GameState.CutScene;

        // SceneManager.LoadScene("CutScene");
    }

    public void Stage2()
    {
        gameState = GameState.Playing;

        // SceneManager.LoadScene("Stage2");
    }

    public bool IsPlaying()
    {
        return gameState == GameState.Playing;
    }
}
