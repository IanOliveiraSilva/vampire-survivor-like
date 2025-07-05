using Survivor.Player.Data;
using Survivor.Weapons;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Survivor.Player
{
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private float currentHealth;
        [SerializeField] private float maxHealth;
        [SerializeField] private float armor;
        [SerializeField] private float healthRegen;


        [SerializeField] private Core.Events.FloatEventChannelSO healthChangedEvent;

        private void Start()
        {
            UpdateHealthBar();
        }

        private void Update()
        {
            if(healthRegen > 0 && currentHealth < maxHealth)
            {
                currentHealth += maxHealth * healthRegen * Time.deltaTime;
                currentHealth = Mathf.Min(currentHealth, maxHealth);
                UpdateHealthBar();
            }
        }
        public void TakeDamage(float amount)
        {
            float reducedAmount = amount * (1 - armor);
            currentHealth -= reducedAmount;

            UpdateHealthBar();
            if(currentHealth < 0)
            {
                currentHealth = 0;
                Die();
            }
        }

        public void Heal(float amount)
        {
            currentHealth += amount;
            if(currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            UpdateHealthBar();
        }

        private void Die()
        {
            Destroy(gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void UpdateHealthBar()
        {
            if(healthChangedEvent != null)
            {
                float normalized = currentHealth / maxHealth;
                healthChangedEvent.Raise(normalized);
            }
        }

        public void SetMaxHealth(float value)
        {
            maxHealth = value;
            currentHealth = maxHealth;
        }

        public void SetArmor(float value)
        {
            armor = Mathf.Clamp01(value);
        }

        public float SetHealthRegen(float value)
        {
            healthRegen = Mathf.Max(0, value);
            return healthRegen;
        }
    }

}
