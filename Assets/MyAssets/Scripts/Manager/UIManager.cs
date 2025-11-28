using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject mainMenuUI;
    public GameObject howToUI;
    public GameObject pauseUI;

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
}
