using Survivor.Weapons;
using Survivor.Weapons.Data;
using UnityEngine;

public abstract class WeaponStrategy : ScriptableObject
{
    public abstract void Attack(Transform attacker, RuntimeWeaponStats weaponStats, int projectileLayer);
}
