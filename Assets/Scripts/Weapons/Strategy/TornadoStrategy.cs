using Survivor.Weapons;
using Survivor.Weapons.Data;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "TornadoStrategy", menuName = "Strategies/TornadoStrategy")]
public class TornadoStrategy : WeaponStrategy
{
    public override void Attack(Transform attacker, RuntimeWeaponStats stats, int projectileLayer)
    {
        // Encontrar o inimigo mais distante do jogador (atacante)
        Collider2D[] enemies = FindEnemies(attacker.position);
        if (enemies.Length == 0) return;

        var farthestEnemy = enemies.OrderByDescending(e => Vector2.Distance(attacker.position, e.transform.position)).First();

        Vector2 direction = (farthestEnemy.transform.position - attacker.position).normalized;

        GameObject tornadoInstance = Instantiate(stats.BaseStats.projectilePrefab, attacker.position, Quaternion.identity);
        tornadoInstance.layer = projectileLayer;

        if (tornadoInstance.TryGetComponent<TornadoProjectile>(out var tornadoProjectile))
        {
            tornadoProjectile.Initialize(stats, direction);
        }
    }

    private Collider2D[] FindEnemies(Vector3 position)
    {
        LayerMask enemyLayer = LayerMask.GetMask("Enemy");
        return Physics2D.OverlapCircleAll(position, 30f, enemyLayer); // raio grande para pegar inimigos
    }
}
