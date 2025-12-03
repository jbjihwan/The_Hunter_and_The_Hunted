using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    public float speed;

    void Update()
    {
        // 매 프레임 -Z 방향으로 이동

        if(transform.position.z < 30f)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
}
