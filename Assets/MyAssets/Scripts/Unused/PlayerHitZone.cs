using UnityEngine;

public class PlayerHitZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // 큐브인지 확인
        CubeQuizItem cube = other.GetComponent<CubeQuizItem>();
        if (cube == null) return;

        // 정답 / 오답 로그 출력
        if (cube.isAnswer)
        {
            Debug.Log("[PlayerHitZone] 정답 큐브와 충돌!  값: " + cube.shownValue);
        }
        else
        {
            Debug.Log("[PlayerHitZone] 오답 큐브와 충돌!   값: " + cube.shownValue);
        }

        // 큐브 삭제
        Destroy(cube.gameObject);
    }
}
