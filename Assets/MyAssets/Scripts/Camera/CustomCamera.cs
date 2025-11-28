using UnityEngine;

public class CustomCamera : MonoBehaviour
{
    public Transform target;
    public float followSpeed;

    void LateUpdate()
    {
        float posX = Mathf.Lerp(transform.position.x, target.position.x, Time.deltaTime * followSpeed);

        transform.position = new Vector3(posX, transform.position.y, transform.position.z);
    }
}
