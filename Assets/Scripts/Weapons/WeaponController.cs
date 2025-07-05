using Survivor.Weapons.Data;
using Survivor.Player;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Survivor.Weapons
{
    public class WeaponController : MonoBehaviour
    {
        [Header("Starting Weapons")]
        [SerializeField]
        private List<WeaponStatsSO> startingWeapons;

        private List<ActiveWeapon> activeWeapons = new List<ActiveWeapon>();
        private int playerProjectileLayer;
        private PlayerMove playerMove;

        private void Awake()
        {
            playerMove = GetComponent<PlayerMove>();
            playerProjectileLayer = LayerMask.NameToLayer("PlayerProjectile");

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
                    weapon.RuntimeStats.CurrentMovementDirection = playerMove.CurrentMoveDirection;
                    weapon.RuntimeStats.BaseStats.attackStrategy.Attack(transform, weapon.RuntimeStats, playerProjectileLayer);
                }
            }
        }

        public void AddNewWeapon(WeaponStatsSO newWeaponStats)
        {
            if (newWeaponStats == null) return;

            activeWeapons.Add(new ActiveWeapon(newWeaponStats));
        }

        public List<WeaponStatsSO> GetActiveWeaponStats()
        {
            
            return activeWeapons.Select(w => w.RuntimeStats.BaseStats).ToList();
        }

        public void AddOrUpgradeWeapon(WeaponStatsSO weaponSO)
        {
            var existing = activeWeapons.FirstOrDefault(w => w.RuntimeStats.BaseStats == weaponSO);

            if (existing != null)
            {
                existing.UpgradeLevel(); // Aumenta 1 nível, até 5
            }
            else
            {
                AddNewWeapon(weaponSO); // Novo RuntimeWeaponStats é criado e adicionado
            }
        }

        public List<RuntimeWeaponStats> GetActiveWeapons()
        {
            return activeWeapons.Select(w => w.RuntimeStats).ToList();
        }




        public class ActiveWeapon
        {
            public RuntimeWeaponStats RuntimeStats { get; private set; }
            private float timer;

            public ActiveWeapon(WeaponStatsSO weaponStats)
            {
                RuntimeStats = new RuntimeWeaponStats(weaponStats);
                timer = 1f / RuntimeStats.GetFireRate();
            }

            public void Tick(float deltaTime)
            {
                timer += deltaTime;
            }

            public bool CanAttack()
            {
                float cooldown = 1f / RuntimeStats.GetFireRate();
                if (timer >= cooldown)
                {
                    timer -= cooldown;
                    return true;
                }
                return false;
            }

            public void UpgradeLevel()
            {
                RuntimeStats.UpgradeLevel();
            }

            // (Opcional) se você quiser comparar se a arma é a mesma
            public WeaponStatsSO GetBaseStats()
            {
                return RuntimeStats.BaseStats;
            }
        }
    }
}
