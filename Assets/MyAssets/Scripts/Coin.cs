using UnityEngine;

public class Coin : MonoBehaviour
{
    public int points = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerEvent player = other.GetComponent<PlayerEvent>();
        if (player == null) return;

        player.OnPickupCoin(points);
        Destroy(gameObject);
    }
}