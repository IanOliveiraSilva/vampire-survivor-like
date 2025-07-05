using System.Collections.Generic;
using UnityEngine;

namespace Survivor.Weapons.Data
{
    [CreateAssetMenu(fileName = "New Weapon Stats", menuName = "Data/WeaponStats")]
    public class WeaponStatsSO : ScriptableObject
    {
        [Header("Informa��es Visuais")]
        public string weaponName;
        public Sprite icon;
        [TextArea] public string description;

        [Header("Efeitos Sonoros")]
        public string attackSoundName;

        [Header("Atributos B�sicos")]
        public GameObject projectilePrefab;

        public float baseDamage = 10f;
        public float baseFireRate = 1f;
        public float baseArea = 1f;
        public float baseProjectileSpeed = 5f;
        public float baseDuration = 3f;
        public int baseProjectileCount = 1;

        [Header("Modificadores de N�vel")]
        public List<WeaponUpgradeLevel> upgradeLevels = new List<WeaponUpgradeLevel>(4);

        [Header("Estrat�gia de Ataque")]
        public WeaponStrategy attackStrategy;


    }
}
