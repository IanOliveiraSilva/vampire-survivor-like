using Survivor.Weapons;
using Survivor.Weapons.Data;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "HomingStrategy", menuName = "Strategies/HomingStrategy")]
public class HomingStrategy : WeaponStrategy
{
    [Tooltip("O raio de busca por inimigos ao redor do jogador.")]
    public float searchRadius = 20f;

    public override void Attack(Transform attacker, RuntimeWeaponStats stats, int projectileLayer)
    {
        if (!string.IsNullOrEmpty(stats.BaseStats.attackSoundName))
        {
            AudioManager.Instance.PlaySFX(stats.BaseStats.attackSoundName);
        }

        Collider2D[] enemiesInRange = FindEnemies(attacker.position);

        if (enemiesInRange.Length == 0)
            return;

        // Ordena inimigos pela distância ao jogador
        var sortedEnemies = enemiesInRange.OrderBy(e => Vector2.Distance(attacker.position, e.transform.position)).ToArray();

        int projectileCount = stats.GetProjectileCount();
        int enemiesToTarget = Mathf.Min(projectileCount, sortedEnemies.Length);

        for (int i = 0; i < projectileCount; i++)
        {
            Collider2D enemyCollider = sortedEnemies[i % sortedEnemies.Length];
            if (enemyCollider == null) continue;

            Transform target = enemyCollider.transform;

            Vector2 direction = (target.position - attacker.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

            GameObject projectileInstance = Instantiate(stats.BaseStats.projectilePrefab, attacker.position, rotation);
            projectileInstance.layer = projectileLayer;

            if (projectileInstance.TryGetComponent<HomingProjectile>(out var homingProjectile))
            {
                homingProjectile.Initialize(stats, target);
            }
        }
    }


    private Collider2D[] FindEnemies(Vector3 searchCenter)
    {
        LayerMask enemyLayer = LayerMask.GetMask("Enemy");
        return Physics2D.OverlapCircleAll(searchCenter, searchRadius, enemyLayer);
    }
}
