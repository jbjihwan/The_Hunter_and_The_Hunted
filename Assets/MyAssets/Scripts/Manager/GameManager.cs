// GameManager의 역할
// 1. 게임의 흐름 제어 (일시 정지, 진행)
// 2. 씬 관리
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool isGameStart { get; private set; }
    public bool isGameOver { get; private set; }
    public bool isGamePaused { get; private set; }
    public bool isGameRunning { get; private set; }

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
            Destroy(this);
        }
    }

    void Start()
    {
        isGameStart = false;
        isGameOver = false;
        isGamePaused = false;
        isGameRunning = false;
        playTime = Time.time;

        UIManager.Instance.OnMainMenuUI();
    }

    public void GameStart()
    {
        isGameStart = true;
        isGameRunning = true;
        UIManager.Instance.OffMainMenuUI();
    }
}
