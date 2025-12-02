using System;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject mainMenuUI;
    public GameObject howToUI;
    public GameObject pauseUI;
    public TextMeshProUGUI timerUI;

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

    }

    void Update()
    {

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
}
