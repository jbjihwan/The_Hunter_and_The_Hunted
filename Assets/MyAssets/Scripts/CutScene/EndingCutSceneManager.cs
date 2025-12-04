using System.Collections;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class EndingCutSceneManager : MonoBehaviour
{
    [Header("Spawners")]
    public PlaneSpawner planeSpawner; // 맵 스포너
    public PlaneSpawner obstacleSpawner; // 장애물 스포너
    
    [Header("Timeline")]
    public PlayableDirector playableDirector; // Timeline 컨트롤러
    public float cutSceneDuration = 15f; // 컷씬 길이
    
    [Header("Spawner Settings")]
    public int planeCycleIndex = 0; // 사용할 맵 사이클 인덱스
    public int obstacleCycleIndex = 0; // 사용할 장애물 사이클 인덱스
    public float obstacleStartDelay = 2f; // 장애물 등장 딜레이
    
    private bool isSkipped = false;

    private void Start()
    {
        // 블링킷 효과와 함께 컷씬 시작
        if (BlinkEffect.Instance != null)
        {
            BlinkEffect.Instance.PlayBlink(1.0f, () => StartEndingCutScene());
        }
        else
        {
            StartEndingCutScene();
        }
    }

    private void StartEndingCutScene()
    {
        // 맵 스포너 활성화
        if (planeSpawner != null)
        {
            planeSpawner.ChangeCycle(planeCycleIndex);
            planeSpawner.enabled = true;
        }
        
        // 장애물 스포너 딜레이 후 활성화
        if (obstacleSpawner != null)
        {
            StartCoroutine(StartObstacleSpawnerWithDelay());
        }
        
        // Timeline 재생
        if (playableDirector != null)
        {
            playableDirector.stopped += OnTimelineEnd;
            playableDirector.Play();
        }
        else
        {
            // Timeline이 없으면 설정된 시간 후 자동 종료
            StartCoroutine(AutoEndCutScene());
        }
    }

    private IEnumerator StartObstacleSpawnerWithDelay()
    {
        yield return new WaitForSeconds(obstacleStartDelay);
        
        if (obstacleSpawner != null)
        {
            obstacleSpawner.ChangeCycle(obstacleCycleIndex);
            obstacleSpawner.enabled = true;
        }
    }

    private void OnTimelineEnd(PlayableDirector director)
    {
        if (!isSkipped)
        {
            EndCutScene();
        }
    }

    private IEnumerator AutoEndCutScene()
    {
        yield return new WaitForSeconds(cutSceneDuration);
        EndCutScene();
    }

    private void EndCutScene()
    {
        isSkipped = true;
        
        // 스포너 비활성화
        if (planeSpawner != null)
        {
            planeSpawner.enabled = false;
        }
        
        if (obstacleSpawner != null)
        {
            obstacleSpawner.enabled = false;
        }
        
        // 게임 종료 또는 크레딧 화면으로 전환
        if (BlinkEffect.Instance != null)
        {
            BlinkEffect.Instance.PlayBlinkWithSceneTransition(0.5f, 0.5f, () =>
            {
                // TODO: 크레딧 씬으로 이동하거나 게임 종료
                Debug.Log("Ending CutScene Finished!");
                // SceneManager.LoadScene("Credits");
                // 또는
                // Application.Quit();
            });
        }
    }

    private void Update()
    {
        // ESC 키로 컷씬 건너뛰기
        if (Input.GetKeyDown(KeyCode.Escape) && !isSkipped)
        {
            SkipCutScene();
        }
    }

    private void SkipCutScene()
    {
        if (playableDirector != null && playableDirector.state == PlayState.Playing)
        {
            playableDirector.Stop();
        }
        
        EndCutScene();
    }
}
