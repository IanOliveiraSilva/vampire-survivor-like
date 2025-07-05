using Survivor.Player;
using UnityEngine;

public class BreadPickup : MonoBehaviour
{
    [SerializeField] private float healAmount = 100f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.TryGetComponent<PlayerHealth>(out var player);
        if (player != null)
        {
            player.Heal(healAmount);
            Destroy(gameObject);
        }
    }
}
