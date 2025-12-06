using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

public class PlaneSpawner : MonoBehaviour
{
    [System.Serializable]
    public class PlaneCycle
    {
        public GameObject[] planeCycle;
    }

    public PlaneCycle[] planeCycles;
    public GameObject endQuad;
    public float planeLength;
    public float endQuadHeight;
    public int planeCount;

    private Queue<GameObject> childPlanes;
    private int cycleIndex;
    private int planeIndex;

    void Awake()
    {
        childPlanes = new Queue<GameObject>();
        cycleIndex = 0;
        planeIndex = 0;
    }

    void Start()
    {
        if (endQuad != null)
        {
            Instantiate(endQuad, transform.position +
            transform.forward * (planeCount * planeLength - planeLength / 2) +
            transform.up * endQuadHeight / 2,
            transform.rotation);
        }
    }

    public void Init()
    {
        Init(-1);
    }

    public void Init(int safe)
    {
        if (planeCycles.Length == 0)
        {
            return;
        }

        safe = Mathf.Min(safe, planeCount + 1);

        for (int i = 0; i < safe; i++)
        {
            SpawnPlane(transform.position + transform.forward * i * planeLength);
        }

        if (safe != -1 && planeCycles.Length > 1)
        {
            ChangeCycle(1);
        }

        for (int i = safe; i <= planeCount; i++)
        {
            SpawnPlane(transform.position + transform.forward * i * planeLength);
        }
    }

    void LateUpdate()
    {
        DestroyPlane();
    }

    public void DestroyPlane()
    {
        if (childPlanes.Count == 0)
        {
            return;
        }

        if (transform.InverseTransformPoint(childPlanes.Peek().transform.position).z < -planeLength)
        {
            GameObject prev = childPlanes.Dequeue();

            SpawnPlane(prev.transform.position + prev.transform.forward * (planeCount + 1) * planeLength);
            Destroy(prev);
        }
    }

    public void SpawnPlane(Vector3 spawnPos)
    {
        childPlanes.Enqueue(Instantiate(planeCycles[cycleIndex].planeCycle[planeIndex], spawnPos, transform.rotation, transform));

        planeIndex = (planeIndex + 1) % planeCycles[cycleIndex].planeCycle.Length;
    }

    public void ChangeCycle(int index)
    {
        cycleIndex = index;
        planeIndex = 0;
    }
}
