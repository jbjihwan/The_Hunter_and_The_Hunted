// Plane의 피봇은 지면의 중심으로 설정
// Obstacle의 피봇은 장애물의 바닥으로 설정
using UnityEngine;

public class Plane : MonoBehaviour
{
    [System.Serializable]
    public class Obstacle
    {
        public GameObject obstaclePrefab;
        public float spawnProb;
        public int spawnIndex;
        public int heightIndex;
    }

    public Transform[] spawnPoints;
    public Obstacle[] obstacles;
    public float speed;
    public float obstacleHeight;

    void Start()
    {
        foreach (Obstacle obstacle in obstacles)
        {
            if (obstacle.spawnProb > Random.Range(0f, 1f))
            {
                Instantiate(obstacle.obstaclePrefab, 
                    spawnPoints[obstacle.spawnIndex].position + 
                    Vector3.up * obstacle.heightIndex * obstacleHeight, 
                    Quaternion.identity, transform);
            }
        }
    }

    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);

        if (transform.position.z < PlaneSpawner.Instance.destroyPosZ)
        {
            PlaneSpawner.Instance.SpawnPlane(transform.position + 
                Vector3.forward * PlaneSpawner.Instance.planeCount * PlaneSpawner.Instance.planeLength);

            Destroy(gameObject);
        }
    }
}
