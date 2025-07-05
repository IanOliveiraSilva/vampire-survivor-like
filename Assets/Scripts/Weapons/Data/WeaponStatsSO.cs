using System.Collections.Generic;
using UnityEngine;

namespace Survivor.Weapons.Data
{
    [CreateAssetMenu(fileName = "New Weapon Stats", menuName = "Data/WeaponStats")]
    public class WeaponStatsSO : ScriptableObject
    {
        [Header("Informações Visuais")]
        public string weaponName;
        public Sprite icon;
        [TextArea] public string description;

        [Header("Efeitos Sonoros")]
        public string attackSoundName;

        [Header("Atributos Básicos")]
        public GameObject projectilePrefab;

        public float baseDamage = 10f;
        public float baseFireRate = 1f;
        public float baseArea = 1f;
        public float baseProjectileSpeed = 5f;
        public float baseDuration = 3f;
        public int baseProjectileCount = 1;

        [Header("Modificadores de Nível")]
        public List<WeaponUpgradeLevel> upgradeLevels = new List<WeaponUpgradeLevel>(4);

        [Header("Estratégia de Ataque")]
        public WeaponStrategy attackStrategy;


    }
}
