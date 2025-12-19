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
        public float heightIndex;
    }

    [System.Serializable]
    public class ObstacleSet
    {
        public Obstacle[] obstacles;
        public bool isMoving;
        public bool isHorizontal;

        public bool useFixedLaneOrder;   // true면 0,1,2 순서대로 고정 스폰

        [Header("Vertical Group Lane Option (Up to 6 obstacles)")]
        public bool useGroupLaneOrder;   //  체크하면 (0,1)(2,3)(4,5) 그룹 레인 지정 사용
        public int[] groupLaneOrder = new int[3] { 0, 1, 2 };
        // groupLaneOrder[0] : (0,1) 그룹이 갈 lane (0~2)
        // groupLaneOrder[1] : (2,3) 그룹이 갈 lane (0~2)
        // groupLaneOrder[2] : (4,5) 그룹이 갈 lane (0~2)
        // 예: [0,1,2] => (0,1)=왼, (2,3)=중, (4,5)=오
        // 예: [1,0,2] => (0,1)=중, (2,3)=왼, (4,5)=오
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
        if (spawnPoints == null || spawnPoints.Length != 3 || obstacleSets == null || obstacleSets.Length == 0)
            return;

        ObstacleSet obstacleSet = obstacleSets[Random.Range(0, obstacleSets.Length)];
        if (obstacleSet == null || obstacleSet.obstacles == null || obstacleSet.obstacles.Length == 0)
            return;

        // 이동 장애물 코드 
        if (obstacleSet.isMoving)
        {
            if (obstacleSet.obstacles[0] == null || obstacleSet.obstacles[0].obstaclePrefab == null) return;

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
            return;
        }

        // 가로 
        if (obstacleSet.isHorizontal)
        {
            for (int i = 0; i < Mathf.Min(obstacleSet.obstacles.Length, 3); i++)
            {
                if (obstacleSet.obstacles[i] == null || obstacleSet.obstacles[i].obstaclePrefab == null) continue;

                if (Random.Range(0f, 1f) <= obstacleSet.obstacles[i].spawnProb)
                {
                    GameObject obstacle = Instantiate(obstacleSet.obstacles[i].obstaclePrefab,
                        spawnPoints[1].position + Vector3.up * obstacleHeight * obstacleSet.obstacles[i].heightIndex,
                        spawnPoints[1].rotation);

                    obstacle.transform.SetParent(transform, true);
                }
            }
            return;
        }

        // ==========================
        // 세로 (여기만 수정/확장)
        // ==========================

        // fixed 체크 안 했을 때만 순서 섞기 (기존 유지)
        if (!obstacleSet.useFixedLaneOrder)
        {
            Helper.Shuffle(spawnPoints);
        }

        //  최대 6개까지만 처리
        int count = Mathf.Min(obstacleSet.obstacles.Length, 6);

        bool useGroup = obstacleSet.useGroupLaneOrder
                        && obstacleSet.groupLaneOrder != null
                        && obstacleSet.groupLaneOrder.Length >= 3;

        for (int i = 0; i < count; i++)
        {
            if (obstacleSet.obstacles[i] == null || obstacleSet.obstacles[i].obstaclePrefab == null) continue;

            if (Random.Range(0f, 1f) <= obstacleSet.obstacles[i].spawnProb)
            {
                int laneIndex;

                if (useGroup)
                {
                    // (0,1)->group0 / (2,3)->group1 / (4,5)->group2
                    int group = i / 2; // 0~2
                    laneIndex = ClampLane(obstacleSet.groupLaneOrder[group]);
                }
                else
                {
                    //  평소처럼 동작 (기존 스타일)
                    laneIndex = i % 3;
                }

                GameObject obstacle = Instantiate(obstacleSet.obstacles[i].obstaclePrefab,
                    spawnPoints[laneIndex].position + Vector3.up * obstacleHeight * obstacleSet.obstacles[i].heightIndex,
                    spawnPoints[laneIndex].rotation);

                obstacle.transform.SetParent(transform, true);
            }
        }
    }

    int ClampLane(int lane)
    {
        if (lane < 0) return 0;
        if (lane > 2) return 2;
        return lane;
    }

    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }
}
