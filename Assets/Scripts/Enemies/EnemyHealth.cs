using UnityEngine;
using Survivor.Weapons;
using Survivor.Enemies.Data;
using Survivor.Pickup;

namespace Survivor.Enemies
{
    public class EnemyHealth : MonoBehaviour, IDamageable
    {
        private float maxHealth = 50f; // Temporario

        [SerializeField]
        private float currentHealth;
        [SerializeField]
        private EnemyStatsSO enemyStats;
        [SerializeField]
        private GameObject xpPickupPrefab;

        private void Awake()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(float amount)
        {
            currentHealth -= amount;
            Debug.Log($"{gameObject.name} took {amount} damage. Health is now {currentHealth}");

            if( currentHealth <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            GameObject pickup = Instantiate(xpPickupPrefab, transform.position, Quaternion.identity);
            if(pickup.TryGetComponent<XPPickup>(out var xpPickup))
            {
                xpPickup.Initialize(enemyStats.xpReward);
            }
            Destroy(gameObject);
        }

        public void SetMaxHealth(float health)
        {
            maxHealth = health;
            currentHealth = maxHealth;
        }
    }

}

