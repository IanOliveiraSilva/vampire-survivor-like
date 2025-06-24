// HomingStrategy.cs
using Survivor.Weapons.Data;
using UnityEngine;

[CreateAssetMenu(fileName = "HomingStrategy", menuName = "Strategies/HomingStrategy")]
public class HomingStrategy : WeaponStrategy
{
    [Tooltip("O raio de busca por inimigos ao redor do jogador.")]
    public float searchRadius = 20f;

    public override void Attack(Transform attacker, WeaponStatsSO stats, int projectileLayer)
    {
        Transform nearestEnemy = FindNearestEnemy(attacker.position);

        if (nearestEnemy == null)
        {
            return;
        }

        for (int i = 0; i < stats.ProjectileCount; i++)
        {
            GameObject projectileInstance = Instantiate(stats.ProjectilePrefab, attacker.position, Quaternion.identity);
            projectileInstance.layer = projectileLayer;

            if (projectileInstance.TryGetComponent<HomingProjectile>(out var homingProjectile))
            {
                homingProjectile.Initialize(stats, nearestEnemy);
            }
        }
    }

    private Transform FindNearestEnemy(Vector3 searchCenter)
    {
        LayerMask enemyLayer = LayerMask.GetMask("Enemy");
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(searchCenter, searchRadius, enemyLayer);

        Transform closestTarget = null;
        float minDistanceSqr = Mathf.Infinity;

        foreach (var enemyCollider in enemiesInRange)
        {
            float distanceSqr = (searchCenter - enemyCollider.transform.position).sqrMagnitude;
            if (distanceSqr < minDistanceSqr)
            {
                minDistanceSqr = distanceSqr;
                closestTarget = enemyCollider.transform;
            }
        }

        return closestTarget;
    }
}