using Survivor.Player.Data;
using Survivor.Weapons;
using UnityEngine;

namespace Survivor.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        private float currentHealth;
        private float maxHealth;

        private PlayerStatsSO playerStats;

        private void Awake()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(float amount)
        {
            currentHealth -= amount;
            Debug.Log($"{gameObject.name} took {amount} damage. Health is now {currentHealth}");

            if(currentHealth < 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Destroy(gameObject);
        }

        public void SetMaxHealth(float health)
        {
            maxHealth = health;
            currentHealth = maxHealth;
        }
    }

}
