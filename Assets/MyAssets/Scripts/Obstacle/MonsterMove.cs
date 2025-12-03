using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    public float speed = 5f;   // 이동 속도

    void Update()
    {
        // 매 프레임 -Z 방향으로 이동
        transform.Translate(transform.forward * speed * Time.deltaTime);
    }
}
