using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform[] cameraPosition;
    public Transform target;
    public float followSpeed;

    void Start()
    {
        ChangePos(0);
    }

    void LateUpdate()
    {
        float posX = Mathf.Lerp(transform.position.x, target.position.x, Time.deltaTime * followSpeed);

        transform.position = new Vector3(posX, transform.position.y, transform.position.z);
    }

    public void ChangePos(int pos)
    {
        transform.position = cameraPosition[pos].position;
        transform.rotation = cameraPosition[pos].rotation;
    }
}
