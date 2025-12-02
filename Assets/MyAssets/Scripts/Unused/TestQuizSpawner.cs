using UnityEngine;

public class TestQuizSpawner : MonoBehaviour
{
    [Header("Spawn Points (3개)")]
    public Transform[] spawnPoints; // 0, 1, 2 총 3개

    [Header("Prefabs")]
    public GameObject answerCubePrefab;  // 정답 큐브 프리팹
    public GameObject wrongCubePrefab;   // 오답 큐브 프리팹

    [Header("Spawn Timing")]
    public float spawnInterval = 3f; // 테스트용: 3초마다 한 세트 스폰

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnQuizSet();
        }
    }

    void SpawnQuizSet()
    {
        if (spawnPoints.Length < 3)
        {
            Debug.LogError("스폰 포인트가 3개 이상 필요합니다.");
            return;
        }

        // 정답 큐브가 들어갈 랜덤 레일
        int answerLane = Random.Range(0, 3);

        for (int i = 0; i < 3; i++)
        {
            Transform sp = spawnPoints[i];

            GameObject cubeObj;

            if (i == answerLane)
            {
                // 정답 큐브
                cubeObj = Instantiate(answerCubePrefab, sp.position, sp.rotation);
            }
            else
            {
                // 오답 큐브
                cubeObj = Instantiate(wrongCubePrefab, sp.position, sp.rotation);
            }

            // 큐브는 OnEnable()에서 QuizManager.RegisterCube() 실행함
        }
    }
}
