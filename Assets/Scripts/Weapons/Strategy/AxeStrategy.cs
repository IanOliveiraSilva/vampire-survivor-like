using Survivor.Weapons;
using UnityEngine;

[CreateAssetMenu(fileName = "AxeStrategy", menuName = "Strategies/AxeStrategy")]
public class AxeStrategy : WeaponStrategy
{
    public override void Attack(Transform attacker, RuntimeWeaponStats stats, int projectileLayer)
    {
        GameObject helperObj = new GameObject("AxeThrowerHelper");
        AxeThrower helper = helperObj.AddComponent<AxeThrower>();

        helper.StartThrowing(
            stats.GetProjectileCount(),
            stats.BaseStats.projectilePrefab,
            stats,
            projectileLayer,
            attacker.position
        );
    }
}
