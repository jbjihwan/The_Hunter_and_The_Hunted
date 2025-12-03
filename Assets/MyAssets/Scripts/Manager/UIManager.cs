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

    public void OnMainMenuUI()
    {
        mainMenuUI.SetActive(true);
    }

    public void OffMainMenuUI()
    {
        mainMenuUI.SetActive(false);
    }

    public void OnHowToUI()
    {
        OffMainMenuUI();
        howToUI.SetActive(true);
    }

    public void OffHowToUI()
    {
        OnMainMenuUI();
        howToUI.SetActive(false);
    }

    public void OnPauseUI()
    {
        pauseUI.SetActive(true);
    }

    public void OffPauseUI()
    {
        pauseUI.SetActive(false);
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
