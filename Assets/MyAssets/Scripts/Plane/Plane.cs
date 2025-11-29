// Plane의 피봇은 지면의 중심으로 설정
// Obstacle의 피봇은 장애물의 바닥으로 설정
using UnityEngine;

public class Plane : MonoBehaviour
{
    [System.Serializable]
    public class Obstacle
    {
        public GameObject obstaclePrefab;
        [Range(0f, 1f)] public float spawnProb;
        public int heightIndex;
    }

    [System.Serializable]
    public class ObstacleSet
    {
        public Obstacle[] obstacles;
    }

    public Transform[] frontSpawnPoints;
    public Transform[] backSpawnPoints;
    public ObstacleSet[] frontObstacleSets;
    public ObstacleSet[] backObstacleSets;
    public float speed;
    public float obstacleHeight;

    void Start()
    {
        ImplantObstacle(frontSpawnPoints, frontObstacleSets);
        ImplantObstacle(backSpawnPoints, backObstacleSets);
    }

    void ImplantObstacle(Transform[] spawnPoints, ObstacleSet[] obstacleSets)
    {
        if (spawnPoints.Length != 3 || obstacleSets.Length == 0)
        {
            return;
        }

        Helper.Shuffle(spawnPoints);
        Obstacle[] obstacleSet = obstacleSets[Random.Range(0, obstacleSets.Length)].obstacles;

        for (int i = 0; i < Mathf.Min(obstacleSet.Length, 3); i++)
        {
            if (Random.Range(0f, 1f) <= obstacleSet[i].spawnProb)
            {
                GameObject obstacle = Instantiate(obstacleSet[i].obstaclePrefab,
                    spawnPoints[i].position + Vector3.up * obstacleHeight * obstacleSet[i].heightIndex,
                    spawnPoints[i].rotation);

                obstacle.transform.SetParent(transform, true);
            }
        }
    }

    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }
}
