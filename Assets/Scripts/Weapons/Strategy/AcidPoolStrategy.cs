using UnityEngine;
using Survivor.Weapons;
using Survivor.Weapons.Data;

[CreateAssetMenu(fileName = "AcidPoolStrategy", menuName = "Strategies/AcidPoolStrategy")]
public class AcidPoolStrategy : WeaponStrategy
{
    public float spawnRadius = 3f;

    public override void Attack(Transform attacker, RuntimeWeaponStats stats, int projectileLayer)
    {
        if (!string.IsNullOrEmpty(stats.BaseStats.attackSoundName))
        {
            AudioManager.Instance.PlaySFX(stats.BaseStats.attackSoundName);
        }

        int poolCount = stats.GetProjectileCount();

        for (int i = 0; i < poolCount; i++)
        {
            Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;
            Vector2 spawnPosition = (Vector2)attacker.position + randomOffset;

            GameObject poolInstance = GameObject.Instantiate(stats.BaseStats.projectilePrefab, spawnPosition, Quaternion.identity);
            poolInstance.layer = projectileLayer;

            if (poolInstance.TryGetComponent<AcidPool>(out var acidPool))
            {
                acidPool.Initialize(stats);
            }
        }
    }
}
