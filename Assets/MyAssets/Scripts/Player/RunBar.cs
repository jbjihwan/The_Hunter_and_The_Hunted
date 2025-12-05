using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// [RunBar]
/// - GameManager의 전체 진행 시간(1~3스테이지)을 기준으로
///   UIManager.Instance.runSlider 값을 0~1 사이로 갱신.
/// </summary>
public class RunBar : MonoBehaviour
{
    // 실제 슬라이더 (인스펙터에서 안 넣어줘도 됨, 자동으로 UIManager에서 가져옴)
    public Slider runSlider;

    private GameManager gm;

    void Start()
    {
        gm = GameManager.Instance;

        // 인스펙터에서 안 넣어줬으면 UIManager 통해서 자동 연결
        if (runSlider == null && UIManager.Instance != null)
        {
            runSlider = UIManager.Instance.runSlider;
        }
    }

    void Update()
    {
        if (gm == null) return;

        //  씬 전환 후 HelperManager가 늦게 세팅되는 경우 대비
        if (runSlider == null && UIManager.Instance != null)
        {
            runSlider = UIManager.Instance.runSlider;
        }

        if (runSlider == null) return;

        float playTime = gm.PlayTime;
        float totalEnd = gm.stage3PlayTime;   // 전체 게임 끝나는 시간(1+2+3)

        float value = Mathf.InverseLerp(0f, totalEnd, playTime);
        runSlider.value = Mathf.Clamp01(value);
    }
}