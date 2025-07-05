using Survivor.Weapons;
using System.Collections;
using UnityEngine;

public class DestructableProp : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 50f;
    private float currentHealth;

    [Header("Drops")]
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject breadPrefab; // pão para dropar
    [SerializeField, Range(0f, 1f)] private float coinDropChance = 0.5f;  // chance de dropar moeda
    [SerializeField, Range(0f, 1f)] private float breadDropChance = 0.3f; // chance de dropar pão
    [SerializeField] private float dropRadius = 1f;

    [SerializeField] private GameObject floatingTextPrefab;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Color originalColor;
    private Coroutine flashCoroutine;

    private void Awake()
    {
        currentHealth = maxHealth;

        if (spriteRenderer == null)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        originalColor = spriteRenderer.color;
    }

    public void TakeDamage(float amount)
    {
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
        if (floatingTextPrefab == null) return;

        Vector3 spawnPos = transform.position + new Vector3(Random.Range(-0.5f, 0.5f), 0, 0);
        GameObject textObj = Instantiate(floatingTextPrefab, spawnPos, Quaternion.identity);
        textObj.transform.SetParent(null);

        if (textObj.TryGetComponent<FloatingDamageText>(out var floatingDamageText))
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

    private void Die()
    {
        TryDropItem();
        Destroy(gameObject);
    }

    private void TryDropItem()
    {
        float roll = Random.value;

        if (roll <= coinDropChance && coinPrefab != null)
        {
            SpawnDrop(coinPrefab);
        }
        else if (roll <= coinDropChance + breadDropChance && breadPrefab != null)
        {
            SpawnDrop(breadPrefab);
        }
    }

    private void SpawnDrop(GameObject prefab)
    {
        Vector3 spawnPos = transform.position;
        Instantiate(prefab, spawnPos, Quaternion.identity);
    }

    public void SetMaxHealth(float health)
    {
        maxHealth = health;
        currentHealth = maxHealth;
    }
}
