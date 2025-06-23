using UnityEngine;
using Survivor.Player.Data;

namespace Survivor.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private PlayerStatsSO playerStats;

        private PlayerMove playerMove;
        private PlayerHealth playerHealth;
        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            playerMove = GetComponent<PlayerMove>();
            playerHealth = GetComponent<PlayerHealth>();
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();

            Initialize();
        }

        private void Initialize()
        {
            if (playerStats == null) return;

            playerMove.SetSpeed(playerStats.MoveSpeed);
            playerHealth.SetMaxHealth(playerStats.MaxHealth);
            spriteRenderer.sprite = playerStats.Sprite;

        }
    }
}

