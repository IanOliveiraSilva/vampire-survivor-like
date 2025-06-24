using Survivor.Weapons.Data;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Survivor.Weapons
{
    public class WeaponController : MonoBehaviour
    {
        [Header("Weapons")]
        [SerializeField]
        private List<ActiveWeapon> activeWeapons;
        [SerializeField]
        private List<WeaponStatsSO> startingWeapons;

        private int playerProjectileLayer;

        private void Awake()
        {
            playerProjectileLayer = LayerMask.NameToLayer("PlayerProjectile");

            activeWeapons = new List<ActiveWeapon>();
            foreach (var weaponStats in startingWeapons)
            {
                activeWeapons.Add(new ActiveWeapon(weaponStats));
            }
        }
        private void Update()
        {
            foreach (var weapon in activeWeapons)
            {
                weapon.Tick(Time.deltaTime);
                if (weapon.CanAttack())
                {
                    weapon.Data.AttackStrategy.Attack(transform, weapon.Data, playerProjectileLayer);
                }
            }
        }
        public void AddWeapon(WeaponStatsSO weaponData)
        {
            if (weaponData == null) return;

            // Cria uma nova ActiveWeapon e a adiciona à lista
            activeWeapons.Add(new ActiveWeapon(weaponData));
        }

        // Método público para que outros scripts possam ver as armas atuais
        public List<WeaponStatsSO> GetActiveWeaponStats()
        {
            List<WeaponStatsSO> statsList = new List<WeaponStatsSO>();
            foreach (var weapon in activeWeapons)
            {
                statsList.Add(weapon.Data);
            }
            return statsList;
        }
        private class ActiveWeapon
        {
            public WeaponStatsSO Data { get; }
            private float timer;

            public ActiveWeapon(WeaponStatsSO data)
            {
                Data = data;
                timer = 1f / Data.FireRate;
            }

            public void Tick(float deltaTime)
            {
                timer += deltaTime;
            }

            public bool CanAttack()
            {
                float cooldown = 1f / Data.FireRate;
                if (timer >= 1f / Data.FireRate)
                {
                    timer -= cooldown;
                    return true;
                }

                return false;
            }
        }
    }
}

