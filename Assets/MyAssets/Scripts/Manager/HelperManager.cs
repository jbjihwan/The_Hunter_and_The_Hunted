using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class HelperManager : MonoBehaviour
{
    public PlaneSpawner planeSpawner;
    public PlaneSpawner obstacleSpawner;
    public PlayerEvent playerEvent;
    public GameObject mainMenuUI;
    public GameObject howToUI;
    public GameObject pauseUI;
    public GameObject difficultyLevelUI;
    public GameObject directionalLight;
    public TextMeshProUGUI timerUI;
    public Slider runSlider;   // RunBar 슬라이더
    //public Slider hpSlider;    // HPBar 슬라이더
    public CircleHpUI circleHpUI;
    public TextMeshProUGUI scoreUI;

    private void Start()
    {
        if(scoreUI != null)
        {
            UIManager.Instance.scoreUI = scoreUI;
        }

        if (planeSpawner != null)
        {
            GameManager.Instance.planeSpawner = planeSpawner;
        }

        if (obstacleSpawner != null)
        {
            GameManager.Instance.obstacleSpawner = obstacleSpawner;
        }

        if(playerEvent != null)
        {
            GameManager.Instance.playerEvent = playerEvent;
        }

        if (directionalLight != null)
        {
            GameManager.Instance.directionalLight = directionalLight;
        }

        if (mainMenuUI != null)
        {
            UIManager.Instance.mainMenuUI = mainMenuUI;
        }

        if (howToUI != null)
        {
            UIManager.Instance.howToUI = howToUI;
        }

        if (pauseUI != null)
        {
            UIManager.Instance.pauseUI = pauseUI;
        }

        if (difficultyLevelUI != null)
        {
            UIManager.Instance.difficultyLevelUI = difficultyLevelUI;
        }

        if (timerUI != null)
        {
            UIManager.Instance.timerUI = timerUI;
        }
        //  슬라이더 연결
        if (runSlider != null)
        {
            UIManager.Instance.runSlider = runSlider;
        }

        
        if (circleHpUI != null)
        {
            UIManager.Instance.circleHpUI = circleHpUI;
        }

        if (SceneManager.GetActiveScene().name == "Stage1")
        {
            GameManager.Instance.InitGame();
        }

        if (SceneManager.GetActiveScene().name == "Stage3")
        {
            GameManager.Instance.planeSpawner.Init();
            GameManager.Instance.obstacleSpawner.Init(GameManager.Instance.safePlaneCount);
            playerEvent.SetHP(GameManager.Instance.maxHP, GameManager.Instance.currentHP);
            UIManager.Instance.OnRunSlider();
            
        }

        if(SceneManager.GetActiveScene().name == "Ending")
        {
            UIManager.Instance.UpdateScore((int)GameManager.Instance.PlayTime +  GameManager.Instance.score);
        }
    }

    public void Stage1()
    {
        GameManager.Instance.Stage1();
    }

    public void Stage3()
    {
        GameManager.Instance.Stage3();
    }

    public void GameRestart()
    {
        GameManager.Instance.GameRestart();
    }

    public void GameResume()
    {
        GameManager.Instance.GameResume();
    }

    public void GameQuit()
    {
        GameManager.Instance.GameQuit();
    }

    public void OnMainMenuUI()
    {
        UIManager.Instance.OnMainMenuUI();
    }

    public void OnHowToUI()
    {
        UIManager.Instance.OnHowToUI();
    }

    public void OffHowToUI()
    {
        UIManager.Instance.OffHowToUI();
    }

    public void OnDifficultyLevelUI()
    {
        UIManager.Instance.OnDifficultyLevelUI();
    }
}
