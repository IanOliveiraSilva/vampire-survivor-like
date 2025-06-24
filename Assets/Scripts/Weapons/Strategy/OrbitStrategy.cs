using Survivor.Weapons.Data;
using UnityEngine;

[CreateAssetMenu(fileName = "OrbitStrategy", menuName = "Strategies/OrbitStrategy")]
public class OrbitStrategy : WeaponStrategy
{
    public override void Attack(Transform attacker, WeaponStatsSO weaponStats, int projectileLayer)
    {
        GameObject projectileInstance = Instantiate(weaponStats.ProjectilePrefab, attacker.position, Quaternion.identity);
        projectileInstance.layer = projectileLayer;

        if(projectileInstance.TryGetComponent<Orbiter>(out var orbiter))
        {
            orbiter.Initialize(attacker, weaponStats);
        }
    }
}
