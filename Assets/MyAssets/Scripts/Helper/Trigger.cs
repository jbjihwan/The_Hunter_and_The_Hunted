using UnityEngine;

public class Trigger : MonoBehaviour
{
    public string targetTag;
    public int trigerCode;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(targetTag))
        {
            GameManager.Instance.Triggered(trigerCode, other.gameObject);
        }
    }
}
