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
        public bool isMoving;
        public bool isHorizontal;
        
        public bool useFixedLaneOrder;   // true면 0,1,2 순서대로 고정 스폰
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

        ObstacleSet obstacleSet = obstacleSets[Random.Range(0, obstacleSets.Length)];

        // 이동 장애물 코드
        if (obstacleSet.isMoving)
        {
            if (obstacleSet.isHorizontal)
            {
                if (Random.Range(0f, 1f) <= obstacleSet.obstacles[0].spawnProb)
                {
                    GameObject obstacle = Instantiate(obstacleSet.obstacles[0].obstaclePrefab,
                        spawnPoints[1].position,
                        spawnPoints[1].rotation);

                    obstacle.transform.SetParent(transform, true);
                }
            }
            else
            {
                if (Random.Range(0f, 1f) <= obstacleSet.obstacles[0].spawnProb)
                {
                    GameObject obstacle = Instantiate(obstacleSet.obstacles[0].obstaclePrefab,
                        spawnPoints[0].position,
                        spawnPoints[0].rotation);

                    obstacle.transform.SetParent(transform, true);
                }
            }
        }
        else
        {
            if (obstacleSet.isHorizontal)
            {
                for (int i = 0; i < Mathf.Min(obstacleSet.obstacles.Length, 3); i++)
                {
                    if (Random.Range(0f, 1f) <= obstacleSet.obstacles[i].spawnProb)
                    {
                        GameObject obstacle = Instantiate(obstacleSet.obstacles[i].obstaclePrefab,
                            spawnPoints[1].position + Vector3.up * obstacleHeight * obstacleSet.obstacles[i].heightIndex,
                            spawnPoints[1].rotation);

                        obstacle.transform.SetParent(transform, true);
                    }
                }
            }
            else
            {
                //  체크 안 했을 때만 순서 섞기
                if (!obstacleSet.useFixedLaneOrder)
                {
                    Helper.Shuffle(spawnPoints);
                }

                for (int i = 0; i < Mathf.Min(obstacleSet.obstacles.Length, 3); i++)
                {
                    if (Random.Range(0f, 1f) <= obstacleSet.obstacles[i].spawnProb)
                    {
                        GameObject obstacle = Instantiate(obstacleSet.obstacles[i].obstaclePrefab,
                            spawnPoints[i].position + Vector3.up * obstacleHeight * obstacleSet.obstacles[i].heightIndex,
                            spawnPoints[i].rotation);

                        obstacle.transform.SetParent(transform, true);
                    }
                }
            }
        }
    }

    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }
}
