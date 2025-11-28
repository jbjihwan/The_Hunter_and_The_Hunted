using UnityEngine;

public class DrawLane : MonoBehaviour
{
    public float laneInterval;
    public float laneLength;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position + transform.right * -laneInterval + transform.forward * -laneLength / 2,
            transform.position + transform.right * -laneInterval + transform.forward * laneLength / 2);

        Gizmos.DrawLine(transform.position + transform.forward * -laneLength / 2,
            transform.position + transform.forward * laneLength / 2);

        Gizmos.DrawLine(transform.position + transform.right * laneInterval + transform.forward * -laneLength / 2,
            transform.position + transform.right * laneInterval + transform.forward * laneLength / 2);
    }
}
