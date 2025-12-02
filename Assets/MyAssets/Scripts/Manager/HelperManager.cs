using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HelperManager : MonoBehaviour
{
    public PlaneSpawner planeSpawner;
    public PlaneSpawner obstacleSpawner;
    public GameObject mainMenuUI;
    public GameObject howToUI;
    public GameObject pauseUI;
    public TextMeshProUGUI timerUI;

    private void Start()
    {
        if (planeSpawner != null)
        {
            GameManager.Instance.planeSpawner = planeSpawner;
        }

        if (obstacleSpawner != null)
        {
            GameManager.Instance.obstacleSpawner = obstacleSpawner;
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

        if (timerUI != null)
        {
            UIManager.Instance.timerUI = timerUI;
        }

        if (SceneManager.GetActiveScene().name == "Stage1")
        {
            GameManager.Instance.InitGame();
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

    public void OnHowToUI()
    {
        UIManager.Instance.OnHowToUI();
    }

    public void OffHowToUI()
    {
        UIManager.Instance.OffHowToUI();
    }
}
