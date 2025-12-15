using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int points;
    public AudioClip coinClip;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddScore(points);
            AudioSource.PlayClipAtPoint(coinClip, transform.position);
            Destroy(gameObject);
        }
    }
}
