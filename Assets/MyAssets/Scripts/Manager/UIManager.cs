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
            Destroy(this);
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

    }

    public void OffHowToUI()
    {

    }
}
