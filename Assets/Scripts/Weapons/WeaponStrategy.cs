using Survivor.Weapons.Data;
using UnityEngine;

public abstract class WeaponStrategy : ScriptableObject
{
    public abstract void Attack(Transform attacker, WeaponStatsSO weaponStats, int projectileLayer);
}
