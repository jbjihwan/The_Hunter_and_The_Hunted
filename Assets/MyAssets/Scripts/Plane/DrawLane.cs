using UnityEngine;

public class DrawLane : MonoBehaviour
{
    public float laneInterval;
    public float laneLength;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position + new Vector3(-laneInterval, 0f, -laneLength / 2f),
            transform.position + new Vector3(-laneInterval, 0f, laneLength / 2f));
        Gizmos.DrawLine(transform.position + new Vector3(0f, 0f, -laneLength / 2f),
            transform.position + new Vector3(0f, 0f, laneLength / 2f));
        Gizmos.DrawLine(transform.position + new Vector3(laneInterval, 0f, -laneLength / 2f),
            transform.position + new Vector3(laneInterval, 0f, laneLength / 2f));
    }
}
