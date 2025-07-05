using UnityEngine;
using Survivor.Weapons;
using Survivor.Enemies.Data;
using Survivor.Pickup;
using System.Collections;
using Survivor.Core.Events;

namespace Survivor.Enemies
{
    public class EnemyHealth : MonoBehaviour, IDamageable
    {
        private float maxHealth = 50f; // Temporário

        [SerializeField] private float currentHealth;

        [SerializeField] private EnemyStatsSO enemyStats;

        [SerializeField] private GameObject xpPickupPrefab;
        [SerializeField] private GameObject floatingTextPrefab;
        [SerializeField] private GameObject chestPrefab;

        [SerializeField] private SpriteRenderer spriteRenderer;


        private Color originalColor;
        private Coroutine flashCoroutine;

        [SerializeField] private IntEventChannelSO onEnemyDeathEvent;

        private void Awake()
        {
            currentHealth = maxHealth;

            if (spriteRenderer == null)
                spriteRenderer = GetComponentInChildren<SpriteRenderer>();

            originalColor = spriteRenderer.color;
        }

        public void TakeDamage(float amount)
        {
            AudioManager.Instance.PlaySFX("enemy_hit");
            currentHealth -= amount;
            ShowDamageNumber(amount);

            FlashRed();

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        private void ShowDamageNumber(float damageAmount)
        {
            Vector3 spawnPos = transform.position + new Vector3(Random.Range(-0.5f, 0.5f), 0, 0);
            GameObject textObj = Instantiate(floatingTextPrefab, spawnPos, Quaternion.identity);

            textObj.transform.SetParent(null);

            if(textObj.TryGetComponent<FloatingDamageText>(out var floatingDamageText))
            {
                floatingDamageText.Setup(damageAmount, Color.white);
            }
        }

        private void FlashRed()
        {
            if (flashCoroutine != null)
                StopCoroutine(flashCoroutine);

            flashCoroutine = StartCoroutine(FlashCoroutine());
        }

        private IEnumerator FlashCoroutine()
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.color = originalColor;
        }

        public void Die()
        {
            if (enemyStats.isBoss)
            {
                GameObject chest = Instantiate(chestPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                GameObject pickup = Instantiate(xpPickupPrefab, transform.position, Quaternion.identity);
                if (pickup.TryGetComponent<XPPickup>(out var xpPickup))
                {
                    xpPickup.Initialize(enemyStats.xpReward);
                }
            }
            onEnemyDeathEvent?.Raise(1);

            Destroy(gameObject);
        }

        public void SetMaxHealth(float health)
        {
            maxHealth = health;
            currentHealth = maxHealth;
        }
    }
}
