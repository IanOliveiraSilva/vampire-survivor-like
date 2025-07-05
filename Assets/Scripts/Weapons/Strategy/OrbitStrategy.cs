using Survivor.Weapons;
using Survivor.Weapons.Data;
using UnityEngine;

[CreateAssetMenu(fileName = "OrbitStrategy", menuName = "Strategies/OrbitStrategy")]
public class OrbitStrategy : WeaponStrategy
{
    private bool hasActiveOrbiters = false;

    public override void Attack(Transform attacker, RuntimeWeaponStats weaponStats, int projectileLayer)
    {
        if (hasActiveOrbiters) return;

        int projectileCount = weaponStats.GetProjectileCount();
        activeOrbitersCount = projectileCount; // aqui incrementa o contador

        float angleStep = 360f / projectileCount;

        for (int i = 0; i < projectileCount; i++)
        {
            GameObject projectileInstance = Instantiate(weaponStats.BaseStats.projectilePrefab, attacker.position, Quaternion.identity);
            projectileInstance.layer = projectileLayer;

            if (projectileInstance.TryGetComponent<Orbiter>(out var orbiter))
            {
                float startAngle = i * angleStep;
                orbiter.Initialize(attacker, weaponStats, startAngle);

                orbiter.OnOrbiterDestroyed += HandleOrbiterDestroyed;
            }
        }

        hasActiveOrbiters = true;
    }


    private int activeOrbitersCount = 0;

    private void HandleOrbiterDestroyed(Orbiter orbiter)
    {
        activeOrbitersCount--;

        if (activeOrbitersCount <= 0)
        {
            hasActiveOrbiters = false;
            activeOrbitersCount = 0;
        }
    }
}
