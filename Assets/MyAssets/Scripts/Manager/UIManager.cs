using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject mainMenuUI;
    public GameObject howToUI;
    public GameObject pauseUI;
    public GameObject difficultyLevelUI;
    public TextMeshProUGUI timerUI;
    public Slider runSlider;   // 진행도 바
    public Slider hpSlider;    // 체력 바

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

    public void OffAllUI()
    {
        OffMainMenuUI();
        OffHowToUI();
        OffPauseUI();
        OffDifficultyLevelUI();
    }

    public void OnMainMenuUI()
    {
        if (mainMenuUI != null)
        {
            OffAllUI();
            mainMenuUI.SetActive(true);
        }
    }

    public void OffMainMenuUI()
    {
        if (mainMenuUI != null)
        {
            mainMenuUI.SetActive(false);
        }
    }

    public void OnHowToUI()
    {
        if (howToUI != null)
        {
            OffAllUI();
            howToUI.SetActive(true);
        }
    }

    public void OffHowToUI()
    {
        if (howToUI != null)
        {
            howToUI.SetActive(false);
        }
    }

    public void OnPauseUI()
    {
        if (pauseUI != null)
        {
            OffAllUI();
            pauseUI.SetActive(true);
        }
    }

    public void OffPauseUI()
    {
        if (pauseUI != null)
        {
            pauseUI.SetActive(false);
        }
    }

    public void OnDifficultyLevelUI()
    {
        if (difficultyLevelUI != null)
        {
            OffAllUI();
            difficultyLevelUI.SetActive(true);
        }
    }

    public void OffDifficultyLevelUI()
    {
        if (difficultyLevelUI != null)
        {
            difficultyLevelUI.SetActive(false);
        }
    }

    public void OnTimerUI()
    {
        timerUI.gameObject.SetActive(true);
    }

    public void OffTimerUI()
    {
        timerUI.gameObject.SetActive(false);
    }

    public void UpdateTimer(float timer)
    {
        if (timerUI != null)
        {
            timerUI.text = TimerFormating(timer);
        }
    }

    public string TimerFormating(float timer)
    {
        TimeSpan ts = TimeSpan.FromSeconds(timer);
        string timerString = string.Format("{0:00}:{1:00}:{2:000}", ts.Minutes, ts.Seconds, ts.Milliseconds);

        return timerString;
    }

    public void OnRunSlider()
    {
        if (runSlider != null)
            runSlider.gameObject.SetActive(true);
    }

    public void OffRunSlider()
    {
        if (runSlider != null)
            runSlider.gameObject.SetActive(false);
    }

    public void OnHpSlider()
    {
        if (hpSlider != null)
            hpSlider.gameObject.SetActive(true);
    }

    public void OffHpSlider()
    {
        if (hpSlider != null)
            hpSlider.gameObject.SetActive(false);
    }
}
