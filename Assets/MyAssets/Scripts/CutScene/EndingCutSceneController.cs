using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class EndingCutSceneController : MonoBehaviour
{
    [Header("Timeline Settings")]
    public PlayableDirector timelineDirector; // EndingCutScene의 Timeline
    
    [Header("UI Settings")]
    public Canvas endingUICanvas; // Fade In할 UI Canvas
    public float delayBeforeFadeIn = 6f; // UI가 나타나기 전 대기 시간
    public float fadeInDuration = 1f; // Fade In 지속 시간

    private List<Graphic> uiGraphics = new List<Graphic>(); // Canvas 내 모든 Graphic 컴포넌트
    private List<Color> originalColors = new List<Color>(); // 원본 색상 저장

    private void Start()
    {
        // Canvas 내 모든 Graphic 컴포넌트 찾기
        if (endingUICanvas != null)
        {
            uiGraphics.AddRange(endingUICanvas.GetComponentsInChildren<Graphic>(true));
            
            // 원본 색상 저장 및 초기 상태로 설정 (완전히 투명)
            foreach (var graphic in uiGraphics)
            {
                originalColors.Add(graphic.color);
                Color color = graphic.color;
                color.a = 0f;
                graphic.color = color;
            }
            
            endingUICanvas.gameObject.SetActive(false);
        }

        // Timeline이 있다면 재생
        if (timelineDirector != null)
        {
            timelineDirector.Play();
        }

        // 6초 후 UI Fade In 시작
        StartCoroutine(FadeInUIAfterDelay());
    }

    private IEnumerator FadeInUIAfterDelay()
    {
        // 지정된 시간만큼 대기
        yield return new WaitForSeconds(delayBeforeFadeIn);

        // UI 활성화 및 Fade In
        if (endingUICanvas != null)
        {
            endingUICanvas.gameObject.SetActive(true);
            yield return StartCoroutine(FadeInUI());
        }
    }

    private IEnumerator FadeInUI()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeInDuration);
            
            // 모든 UI 요소의 alpha 값 조정
            for (int i = 0; i < uiGraphics.Count; i++)
            {
                if (uiGraphics[i] != null)
                {
                    Color color = originalColors[i];
                    color.a = originalColors[i].a * alpha; // 원본 alpha 값 비율로 조정
                    uiGraphics[i].color = color;
                }
            }
            
            yield return null;
        }

        // 최종적으로 원본 색상으로 복원
        for (int i = 0; i < uiGraphics.Count; i++)
        {
            if (uiGraphics[i] != null)
            {
                uiGraphics[i].color = originalColors[i];
            }
        }
    }
}
