using UnityEngine;

namespace Survivor.Weapons.Data
{
    [CreateAssetMenu(fileName="New Weapon Stats", menuName="Data/WeaponStats")]
    public class WeaponStatsSO : ScriptableObject
    {
        [Header("Info")]
        public string Name;
        public string Description;
        public Sprite Icon;

        [Header("Stats")]
        public float Damage = 10f;
        public float FireRate = 1f;
        public int ProjectileCount = 1;
        public float ProjectileSpeed = 10f;

        [Header("Assets")]
        public GameObject ProjectilePrefab;
    }
}