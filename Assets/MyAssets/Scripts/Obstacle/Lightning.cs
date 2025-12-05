using UnityEngine;

public class Lightning : MonoBehaviour
{
    public Vector3 moveOffset;
    public float speed;

    private Vector3 previousOffset;

    void Start()
    {
        previousOffset = Vector3.zero;
        speed = Random.Range(0.5f, 2.0f) * speed;
    }

    void Update()
    {
        float progress = Mathf.PingPong(Time.time * speed, 1.0f);
        Vector3 currentOffset = moveOffset * progress;
        Vector3 delta = currentOffset - previousOffset;

        transform.position += delta;
        previousOffset = currentOffset;
    }
}
