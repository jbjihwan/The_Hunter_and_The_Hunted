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
    }

    public Transform[] spawnPoints;
    public Obstacle[] obstacles;
    public float speed;

    void Start()
    {
        foreach (Obstacle obstacle in obstacles)
        {
            if (obstacle.spawnProb > Random.Range(0f, 1f))
            {
                Instantiate(obstacle.obstaclePrefab, spawnPoints[obstacle.spawnIndex].position, Quaternion.identity, transform);
            }
        }
    }

    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }
}
