using Survivor.Enemies.Data;
using Survivor.Player;
using UnityEngine;

namespace Survivor.Enemies
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField]
        private EnemyStatsSO enemyStats;
        private float lastAttackTime = 0f;

        private void OnCollisionStay2D(Collision2D other)
        {
            if(other.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
            {
                if (Time.time >= lastAttackTime + enemyStats.attackCooldown)
                {
                    playerHealth.TakeDamage(enemyStats.contactDamage);
                    lastAttackTime = Time.time;
                    Debug.Log($"{gameObject.name} attacked {other.gameObject.name} for {enemyStats.contactDamage} damage.");
                }
            }
        }
    }

}

