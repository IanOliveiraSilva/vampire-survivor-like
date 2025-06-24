using Survivor.Weapons.Data;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponPool", menuName = "Upgrades/Weapon Pool")]
public class WeaponPoolSO : ScriptableObject
{
    [Tooltip("Lista de todas as armas que podem ser oferecidas como upgrade.")]
    public List<WeaponStatsSO> availableWeapons;
}