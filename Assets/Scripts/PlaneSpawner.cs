// 시작 타일이 너무 길면 주석 부분 수정 필요
using UnityEngine;

public class PlaneSpawner : MonoBehaviour
{
    public static PlaneSpawner Instance;
    public GameObject[] planes;
    public GameObject endQuad;
    public float destroyPosZ { get; private set; }
    public float planeLength;
    public float endQuadHeight;
    public int planeCount;
    //public int startPlaneCount;

    private int planeIndex;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        destroyPosZ = transform.position.z - planeLength;
        planeIndex = 0;

        for (int i = 0; i <= planeCount; i++)
        {
            //if (i >= startPlaneCount)
            //{
            //    ChangeIndex(Random.Range(0, planes.Length));
            //}

            SpawnPlane(transform.position + Vector3.forward * i * planeLength);
        }

        Instantiate(endQuad, transform.position +
            Vector3.forward * (planeCount * planeLength - planeLength / 2) +
            Vector3.up * endQuadHeight / 2,
            Quaternion.identity);
    }

    public void SpawnPlane(Vector3 spawnPos)
    {
        Instantiate(planes[planeIndex], spawnPos, Quaternion.identity);
    }

    public void ChangeIndex(int index)
    {
        planeIndex = index;
    }
}
