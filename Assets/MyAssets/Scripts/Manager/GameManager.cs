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
    public PlaneSpawner planeSpawner;
    public float stage1PlayTime;
    public float stage2PlayTime;
    public float stage3PlayTime;

    private GameState gameState;
    private float playTime;
    private int stage;

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

    void Update()
    {
        if (IsPlaying() && Input.GetKeyDown(KeyCode.Escape))
        {
            GamePause();
        }

        if (IsPlaying())
        {
            playTime += Time.deltaTime;
        }

        if (stage == 1 && IsPlaying() && playTime > stage1PlayTime)
        {
            Stage2();
        }

        if (stage == 2 && IsPlaying() && playTime > stage2PlayTime)
        {
            PlayCutScene();
        }

        if (stage == 3 && IsPlaying() && playTime > stage3PlayTime)
        {
            GameEnding();
        }
    }

    public bool IsPlaying()
    {
        return gameState == GameState.Playing;
    }

    public void InitGame()
    {
        gameState = GameState.Ready;
        playTime = 0f;
        stage = 0;

        UIManager.Instance.OnMainMenuUI();
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

        SceneManager.LoadScene("GameOver");
    }

    public void GameEnding()
    {
        SceneManager.LoadScene("Ending");
    }

    public void GameRestart()
    {
        SceneManager.LoadScene("Stage1");

        InitGame();
    }

    // 게임 흐름

    public void Stage1()
    {
        gameState = GameState.Playing;
        stage = 1;

        UIManager.Instance.OffMainMenuUI();
        planeSpawner.ChangeCycle(1);
    }

    public void Stage2()
    {
        stage = 2;

        planeSpawner.ChangeCycle(2);
    }

    public void PlayCutScene()
    {
        gameState = GameState.CutScene;

        SceneManager.LoadScene("CutScene");
    }

    public void Stage3()
    {
        gameState = GameState.Playing;

        SceneManager.LoadScene("Stage3");
    }
}
