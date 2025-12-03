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
        CutScene,
        Ending
    }

    public static GameManager Instance;
    public PlaneSpawner planeSpawner;
    public PlaneSpawner obstacleSpawner;
    public float stage1PlayTime;
    public float stage2SafeTime;
    public float stage2PlayTime;
    public float stage3SafeTime;
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

    private void Start()
    {
        stage2SafeTime = stage1PlayTime + stage2SafeTime;
        stage2PlayTime = stage1PlayTime + stage2PlayTime;
        stage3SafeTime = stage2PlayTime + stage3SafeTime;
        stage3PlayTime = stage2PlayTime + stage3PlayTime;
    }

    void Update()
    {
        if ((IsPlaying() || gameState == GameState.CutScene) && Input.GetKeyDown(KeyCode.Escape))
        {
            GamePause();
        }

        if (IsPlaying())
        {
            playTime += Time.deltaTime;
            // UIManager.Instance.UpdateTimer(playTime);
        }

        if (stage == 1 && IsPlaying() && playTime > stage1PlayTime)
        {
            Stage2();
        }

        if (stage == 2 && IsPlaying() && playTime > stage2SafeTime)
        {
            planeSpawner.ChangeCycle(3);
        }

        if (stage == 2 && IsPlaying() && playTime > stage2PlayTime)
        {
            PlayCutScene();
        }

        if(stage == 3 && IsPlaying() && playTime > stage3SafeTime)
        {
            obstacleSpawner.ChangeCycle(1);
        }

        if (stage == 3 && IsPlaying() && playTime > stage3PlayTime)
        {
            planeSpawner.ChangeCycle(1);
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

    public void GameRestart()
    {
        SceneManager.LoadScene("Stage1");
    }

    // 게임 흐름

    public void Stage1()
    {
        gameState = GameState.Playing;
        stage = 1;

        UIManager.Instance.OffMainMenuUI();
        UIManager.Instance.OnRunSlider();
        UIManager.Instance.OnHpSlider();
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
        stage = 3;

        SceneManager.LoadScene("Stage3");
    }

    public void GameEnding()
    {
        gameState = GameState.Ending;

        SceneManager.LoadScene("Ending");
    }

    // RunBar에서 거리 계산에 사용
    public float PlayTime => playTime;
    public int CurrentStage => stage;
}
