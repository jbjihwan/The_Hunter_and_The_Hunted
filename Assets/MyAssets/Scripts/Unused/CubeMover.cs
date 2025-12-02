using UnityEngine;

public class CubeMover : MonoBehaviour
{
    public float speed = 5f;   // 이동 속도 (원하면 조절 가능)

    void Update()
    {
        transform.Translate(-Vector3.forward * speed * Time.deltaTime);
    }
}
