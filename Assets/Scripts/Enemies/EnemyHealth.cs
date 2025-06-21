using UnityEngine;
using Survivor.Weapons;

namespace Survivor.Enemies
{
    public class EnemyHealth : MonoBehaviour, IDamageable
    {
        private float maxHealth = 50f; // Temporario
        private float currentHealth;

        private void Awake()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(float amount)
        {
            currentHealth -= amount;
            Debug.Log($"{gameObject.name} took {amount} damage. Health is now {currentHealth}");

            if( currentHealth < 0)
            {
                Die();
            }
        }

        public void Die()
        {
            Debug.Log($"{gameObject.name} has died.");
            Destroy(gameObject);
        }

        public void SetMaxHealth(float health)
        {
            maxHealth = health;
            currentHealth = maxHealth;
        }
    }

}

