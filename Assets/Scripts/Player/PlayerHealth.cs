using Survivor.Player.Data;
using Survivor.Weapons;
using UnityEngine;

namespace Survivor.Player
{
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        [SerializeField]
        private float currentHealth;

        [SerializeField]
        private PlayerStatsSO playerStats;

        [SerializeField]
        private Core.Events.FloatEventChannelSO healthChangedEvent;

        private void Awake()
        {
            currentHealth = playerStats.MaxHealth;
        }

        public void TakeDamage(float amount)
        {
            currentHealth -= amount;
            Debug.Log($"{gameObject.name} took {amount} damage. Health is now {currentHealth}");


            UpdateHealthBar();
            if(currentHealth < 0)
            {
                currentHealth = 0;
                Die();
            }
        }

        private void Die()
        {
            Destroy(gameObject);
        }

        private void UpdateHealthBar()
        {

            float normalizedHealth = currentHealth / playerStats.MaxHealth;
            healthChangedEvent.Raise(normalizedHealth);
            Debug.Log($"Health changed: {currentHealth}");
        }
    }

}
