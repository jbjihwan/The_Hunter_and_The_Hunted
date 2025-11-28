using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

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

    }

    public void OffMainMenuUI()
    {

    }

    public void OnHowToUI()
    {
        OffMainMenuUI();
    }

    public void OffHowToUI()
    {
        OnMainMenuUI();
    }

    public void OnPauseUI()
    {

    }

    public void OffPauseUI()
    {

    }
}
