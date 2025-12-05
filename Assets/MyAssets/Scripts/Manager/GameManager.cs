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
    public GameObject directionalLight;
    public PlayerEvent playerEvent;
    public float stage1PlayTime;
    public float stage2PlayTime;
    public float stage3PlayTime;
    public int safePlaneCount;
    public int maxHP;
    public int currentHP;

    private GameState gameState;
    private float stage2SafeTime;
    private float stage3SafeTime;
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
        stage2SafeTime = stage1PlayTime + 5f; // safe time -> 안정적으로 던전 입구 -> 통로로 변환하는데 필요한 시간
        stage2PlayTime = stage1PlayTime + stage2PlayTime;
        stage3SafeTime = stage2PlayTime + 0f; // safe time -> 첫 전기 공격이 오지 않는 시간인데 이제 필요 없음
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
            planeSpawner.ChangeCycle(2);
        }

        if (stage == 2 && IsPlaying() && playTime > stage2PlayTime)
        {
            PlayCutScene();
        }

        if (stage == 3 && IsPlaying() && playTime > stage3SafeTime)
        {
            obstacleSpawner.ChangeCycle(1);
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
        Time.timeScale = 1f;
        // 시작하자마자 Stage0 BGM 재생
        SoundManager.instance.PlayStageBgm(0);
        planeSpawner.Init();
        UIManager.Instance.OnMainMenuUI();
    }

    public void GamePause()
    {
        gameState = GameState.Paused;
        Time.timeScale = 0f;
        //  Pause BGM
        if (SoundManager.instance != null)
            SoundManager.instance.PauseBgm();
        UIManager.Instance.OnPauseUI();
        UIManager.Instance.OffHpSlider();
        UIManager.Instance.OffRunSlider();
    }

    public void GameResume()
    {
        gameState = GameState.Playing;
        Time.timeScale = 1f;
        //  Resume BGM
        if (SoundManager.instance != null)
            SoundManager.instance.ResumeBgm();
        UIManager.Instance.OffPauseUI();
        UIManager.Instance.OnHpSlider();
        UIManager.Instance.OnRunSlider();
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

    public void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }

    // 게임 흐름

    public void Stage1()
    {
        gameState = GameState.Playing;
        stage = 1;

        UIManager.Instance.OffMainMenuUI();
        UIManager.Instance.OnRunSlider();
        UIManager.Instance.OnHpSlider();
        obstacleSpawner.Init(safePlaneCount);
    }

    public void Stage2()
    {
        stage = 2;

        planeSpawner.ChangeCycle(1);
        obstacleSpawner.ChangeCycle(2);
    }

    public void PlayCutScene()
    {
        gameState = GameState.CutScene;

        SceneManager.LoadScene("CutScene");
    }

    public void Stage3()
    {
        maxHP = playerEvent.maxHP;
        currentHP = playerEvent.currentHP;
        gameState = GameState.Playing;
        stage = 3;

        SceneManager.LoadScene("Stage3");
        // 씬 로드 후 바로 BGM 재생
        SoundManager.instance.PlayStageBgm(2);
    }

    public void GameEnding()
    {
        gameState = GameState.Ending;

        SceneManager.LoadScene("Ending");
    }

    public void Triggered(int code, GameObject go)
    {
        if (code == 1)
        {
            // Monster 제거 트리거
            Destroy(go);
        }
        else if (code == 2)
        {
            // 스테이지 2 시작 트리거
            directionalLight.SetActive(false);
            SoundManager.instance.PlayStageBgm(1);
        }
        else if (code == 3)
        {
            // 데미지 트리거
            if(go.CompareTag("Player"))
            {
                go.GetComponent<PlayerEvent>().TakeDamage(1);
            }
        }
    }

    // RunBar에서 거리 계산에 사용
    public float PlayTime => playTime;
    public int CurrentStage => stage;
}
