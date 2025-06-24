using Survivor.Weapons;
using Survivor.Weapons.Data;
using System.Collections.Generic;
using UnityEngine;

public class Aura : MonoBehaviour
{
    private WeaponStatsSO weaponStats;

    private float damageTickCooldown;
    private float lastDamageTick;

    private List<IDamageable> targetsInRange = new List<IDamageable>();

    public void Initialize(WeaponStatsSO _weaponStats)
    {
        weaponStats = _weaponStats;
        damageTickCooldown = 1f / weaponStats.FireRate;
    }

    private void Update()
    {
        if (Time.time > lastDamageTick + damageTickCooldown)
        {
           foreach (var target in targetsInRange)
            {
                target.TakeDamage(weaponStats.Damage);
            }
            lastDamageTick = Time.time;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IDamageable>(out var damageable))
        {
            if (!targetsInRange.Contains(damageable))
            {
                targetsInRange.Add(damageable);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<IDamageable>(out var damageable))
        {
            targetsInRange.Remove(damageable);
        }
    }
}
