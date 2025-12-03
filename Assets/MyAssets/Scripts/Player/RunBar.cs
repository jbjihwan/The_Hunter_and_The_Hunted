using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// [RunBar]
/// - GameManager의 진행 시간 / 스테이지 정보 기반으로
///   UIManager.Instance.runSlider 값을 0~1 사이로 갱신.
/// </summary>
public class RunBar : MonoBehaviour
{
    // 실제 슬라이더 (인스펙터에서 안 넣어줘도 됨, 자동으로 UIManager에서 가져옴)
    public Slider runSlider;

    private GameManager gm;

    void Start()
    {
        // GameManager, UIManager 싱글톤 참조
        gm = GameManager.Instance;

        // runSlider를 인스펙터에서 안 넣었으면 UIManager에서 자동으로 가져온다.
        if (runSlider == null && UIManager.Instance != null)
        {
            runSlider = UIManager.Instance.runSlider;
        }
    }

    void Update()
    {
        if (gm == null || runSlider == null) return;

        float playTime = gm.PlayTime;
        float value = 0f;

        // 1번째 사이클: Stage1 + Stage2 (safe + play)
        // 0 ~ stage2PlayTime 를 0~1로 매핑
        if (gm.CurrentStage < 3)
        {
            float end = gm.stage2PlayTime;
            value = Mathf.InverseLerp(0f, end, playTime);
        }
        // 2번째 사이클: Stage3 (safe + play)
        // stage2PlayTime ~ stage3PlayTime 를 0~1로 매핑
        else
        {
            float start = gm.stage2PlayTime;
            float end = gm.stage3PlayTime;
            value = Mathf.InverseLerp(start, end, playTime);
        }

        runSlider.value = Mathf.Clamp01(value);
    }
}
