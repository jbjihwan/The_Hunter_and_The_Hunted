using UnityEngine;

public class Heart : MonoBehaviour
{
    public int healAmount = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerEvent player = other.GetComponent<PlayerEvent>();
        if (player == null) return;

        player.OnPickupHeart(healAmount);
        Destroy(gameObject);
    }
}