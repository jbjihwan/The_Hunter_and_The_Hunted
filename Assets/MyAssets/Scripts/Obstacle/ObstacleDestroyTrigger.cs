using UnityEngine;

public class ObstacleDestroyTrigger : MonoBehaviour
{
    // 트리거에 들어온 애가 "Obstacle" 태그면 그 오브젝트를 제거
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            Destroy(other.gameObject);
        }
    }
}