using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BlinkEffect : MonoBehaviour
{
    public static BlinkEffect Instance;

    private Image blackScreen;
    private Canvas canvas;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SetupBlinkEffect();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void SetupBlinkEffect()
    {
        // Canvas 설정
        canvas = gameObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 9999; // 최상위 렌더링

        // Canvas Scaler 추가
        CanvasScaler scaler = gameObject.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);

        // Graphic Raycaster 추가
        gameObject.AddComponent<GraphicRaycaster>();

        // 검정색 Image 생성
        GameObject imageObj = new GameObject("BlackScreen");
        imageObj.transform.SetParent(transform);
        
        blackScreen = imageObj.AddComponent<Image>();
        blackScreen.color = new Color(0, 0, 0, 0); // 초기에는 투명
        
        RectTransform rectTransform = blackScreen.GetComponent<RectTransform>();
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.sizeDelta = Vector2.zero;
        rectTransform.anchoredPosition = Vector2.zero;

        // 초기에는 비활성화
        blackScreen.gameObject.SetActive(false);
    }

    /// <summary>
    /// 블링킷 효과 재생 (페이드 아웃 → 페이드 인)
    /// </summary>
    /// <param name="blinkDuration">전체 블링킷 지속 시간</param>
    /// <param name="onComplete">블링킷 완료 후 콜백</param>
    public void PlayBlink(float blinkDuration = 1.0f, System.Action onComplete = null)
    {
        StartCoroutine(BlinkCoroutine(blinkDuration, onComplete));
    }

    /// <summary>
    /// 씬 전환을 위한 블링킷 효과 (페이드 아웃 → 중간 콜백 → 페이드 인)
    /// </summary>
    /// <param name="fadeOutDuration">페이드 아웃 시간</param>
    /// <param name="fadeInDuration">페이드 인 시간</param>
    /// <param name="onMiddle">페이드 아웃 완료 후 실행할 콜백 (씬 전환 등)</param>
    public void PlayBlinkWithSceneTransition(float fadeOutDuration = 0.5f, float fadeInDuration = 0.5f, System.Action onMiddle = null)
    {
        StartCoroutine(BlinkWithSceneTransitionCoroutine(fadeOutDuration, fadeInDuration, onMiddle));
    }

    private IEnumerator BlinkCoroutine(float duration, System.Action onComplete)
    {
        blackScreen.gameObject.SetActive(true);

        float halfDuration = duration / 2f;

        // 페이드 아웃 (투명 → 검정)
        yield return StartCoroutine(FadeOut(halfDuration));

        // 페이드 인 (검정 → 투명)
        yield return StartCoroutine(FadeIn(halfDuration));

        blackScreen.gameObject.SetActive(false);
        onComplete?.Invoke();
    }

    private IEnumerator BlinkWithSceneTransitionCoroutine(float fadeOutDuration, float fadeInDuration, System.Action onMiddle)
    {
        blackScreen.gameObject.SetActive(true);

        // 페이드 아웃 (투명 → 검정)
        yield return StartCoroutine(FadeOut(fadeOutDuration));

        // 중간 작업 실행 (씬 로드 등)
        onMiddle?.Invoke();

        // 씬 로드 대기 (다음 프레임까지)
        yield return null;

        // 페이드 인 (검정 → 투명)
        yield return StartCoroutine(FadeIn(fadeInDuration));

        blackScreen.gameObject.SetActive(false);
    }

    private IEnumerator FadeOut(float duration)
    {
        float elapsedTime = 0f;
        Color color = blackScreen.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime; // unscaledDeltaTime 사용 (Time.timeScale 영향 안 받음)
            float alpha = Mathf.Clamp01(elapsedTime / duration);
            blackScreen.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        blackScreen.color = new Color(0, 0, 0, 1);
    }

    private IEnumerator FadeIn(float duration)
    {
        float elapsedTime = 0f;
        Color color = blackScreen.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float alpha = 1f - Mathf.Clamp01(elapsedTime / duration);
            blackScreen.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        blackScreen.color = new Color(0, 0, 0, 0);
    }
}
