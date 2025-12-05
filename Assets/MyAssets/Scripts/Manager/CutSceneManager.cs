using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class CutSceneManager : MonoBehaviour
{
    public VideoPlayer videoPlayer; // 비디오 플레이어 (유니티에서 할당)
    public float cutSceneDuration = 10f; // 비디오가 없을 경우 대기 시간

    private void Start()
    {
        // 블링킷 효과와 함께 컷씬 시작
        if (BlinkEffect.Instance != null)
        {
            BlinkEffect.Instance.PlayBlink(1.0f, () => StartCutScene());
        }
        else
        {
            StartCutScene();
        }
    }

    private void StartCutScene()
    {
        if (videoPlayer != null)
        {
            // 비디오 플레이어 설정
            videoPlayer.loopPointReached += OnVideoEnd;
            videoPlayer.Play();
        }
        else
        {
            // 비디오가 없으면 설정된 시간 후 자동으로 Stage3로 이동
            StartCoroutine(AutoProgressToStage3());
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        // 비디오 종료 시 Stage3로 이동
        TransitionToStage3();
    }

    private IEnumerator AutoProgressToStage3()
    {
        yield return new WaitForSeconds(cutSceneDuration);
        TransitionToStage3();
    }

    private void TransitionToStage3()
    {
        if (BlinkEffect.Instance != null)
        {
            // 블링킷 효과와 함께 Stage3로 전환
            BlinkEffect.Instance.PlayBlinkWithSceneTransition(0.5f, 0.5f, () =>
            {
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.Stage3();
                }
            });
        }
        else
        {
            // BlinkEffect가 없으면 바로 Stage3로 이동
            if (GameManager.Instance != null)
            {
                GameManager.Instance.Stage3();
            }
        }
    }

    private void Update()
    {
        // ESC 키로 컷씬 건너뛰기
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SkipCutScene();
        }
    }

    private void SkipCutScene()
    {
        if (videoPlayer != null && videoPlayer.isPlaying)
        {
            videoPlayer.Stop();
        }
        TransitionToStage3();
    }
}
