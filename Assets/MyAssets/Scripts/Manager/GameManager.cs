// GameManager의 역할
// 1. 게임의 흐름 제어 (일시 정지, 진행)
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool isGameStart { get; private set; }
    public bool isGameOver { get; private set; }
    public bool isGamePaused { get; private set; }

    private float playTime;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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
        playTime = Time.time;
    }

    public void GameStart()
    {
        isGameStart = true;

        PlaneSpawner.Instance.ChangeIndex(1);
    }
}
