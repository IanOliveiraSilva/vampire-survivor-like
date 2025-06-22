using Survivor.Weapons.Data;
using System.Collections.Generic;
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

        private void Awake()
        {
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
                    Attack(weapon.Data);
                }
            }
        }
        private void Attack(WeaponStatsSO weaponStats)
        {
            if (weaponStats.ProjectilePrefab == null) return;

            // Instancia o projétil na posição do jogador
            GameObject projectile = Instantiate(weaponStats.ProjectilePrefab, transform.position, Quaternion.identity);

            // Pega o componente Projectile e o inicializa com os dados da arma
            if (projectile.TryGetComponent<Projectile>(out var _projectile))
            {
                // Por enquanto, todos os projéteis vão para a direita como exemplo
                Vector2 direction = Vector2.right;
                _projectile.Initialize(weaponStats.ProjectileSpeed, weaponStats.Damage, direction);
            }
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
                if (timer >= 1f / Data.FireRate)
                {
                    timer = 0f;
                    return true;
                }

                return false;
            }
        }
    }
}

