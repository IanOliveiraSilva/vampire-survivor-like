using Survivor.Enemies.Data;
using UnityEngine;

namespace Survivor.Enemies
{
    public class EnemyController : MonoBehaviour
    {
        private EnemyStatsSO enemyStats;

        private EnemyMove enemyMove;
        private EnemyHealth enemyHealth;
        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            enemyMove = GetComponent<EnemyMove>();
            enemyHealth = GetComponent<EnemyHealth>();
            spriteRenderer = GetComponent<SpriteRenderer>();

            Initialize();
        }

        private void Initialize()
        {
            if (enemyStats == null) return;

            enemyMove.SetSpeed(enemyStats.MoveSpeed);
            enemyHealth.SetMaxHealth(enemyStats.MaxHealth);
            spriteRenderer.sprite = enemyStats.Sprite;

        }
    }
}

