using UnityEngine;

public class Lightning : MonoBehaviour
{
    public Vector3 moveOffset;
    public float speed;

    private Vector3 previousOffset;

    void Start()
    {
        previousOffset = Vector3.zero;
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
