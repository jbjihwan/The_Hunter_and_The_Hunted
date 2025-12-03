using UnityEngine;

public class Trap : MonoBehaviour
{
    [Header("Damage Settings")]
    public int contactDamage = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        // Try to get PlayerEvent component
        PlayerEvent playerEvent = other.GetComponent<PlayerEvent>();

        // Apply damage if PlayerEvent exists
        if (playerEvent != null)
        {
            playerEvent.OnTrapHit(contactDamage);
        }
    }
}
