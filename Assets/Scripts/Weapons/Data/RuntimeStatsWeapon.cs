using Survivor.Weapons.Data;
using UnityEngine;

namespace Survivor.Weapons
{
    public class RuntimeWeaponStats
    {
        public WeaponStatsSO BaseStats { get; private set; }

        private float currentDamage;
        private float currentFireRate;
        private float currentArea;
        private float currentProjectileSpeed;
        private float currentDuration;
        private int currentProjectileCount;

        public int CurrentLevel { get; private set; }

        public RuntimeWeaponStats(WeaponStatsSO weaponStats)
        {
            BaseStats = weaponStats;

            // Inicializa no valor base (sem upgrades)
            currentDamage = BaseStats.baseDamage;
            currentFireRate = BaseStats.baseFireRate;
            currentArea = BaseStats.baseArea;
            currentProjectileSpeed = BaseStats.baseProjectileSpeed;
            currentDuration = BaseStats.baseDuration;
            currentProjectileCount = BaseStats.baseProjectileCount;
        }

        private void ApplyUpgrades()
        {
            // Soma os multiplicadores percentuais de todos os upgrades até o nível atual
            float damageMultiplier = 0f;
            float fireRateMultiplier = 0f;
            float areaMultiplier = 0f;
            float projectileSpeedMultiplier = 0f;
            float durationMultiplier = 0f;
            int additionalProjectiles = 0;

            for (int i = 0; i < CurrentLevel; i++)
            {
                var upgrade = BaseStats.upgradeLevels[i];
                damageMultiplier += upgrade.damageModifier;
                fireRateMultiplier += upgrade.fireRateModifier;
                areaMultiplier += upgrade.areaModifier;
                projectileSpeedMultiplier += upgrade.projectileSpeedModifier;
                durationMultiplier += upgrade.durationModifier;
                additionalProjectiles += upgrade.additionalProjectiles;
            }

            // Aplica multiplicadores percentuais no valor base
            currentDamage = BaseStats.baseDamage * (1 + damageMultiplier);
            currentFireRate = BaseStats.baseFireRate * (1 + fireRateMultiplier);
            currentArea = BaseStats.baseArea * (1 + areaMultiplier);
            currentProjectileSpeed = BaseStats.baseProjectileSpeed * (1 + projectileSpeedMultiplier);
            currentDuration = BaseStats.baseDuration * (1 + durationMultiplier);
            currentProjectileCount = BaseStats.baseProjectileCount + additionalProjectiles;
        }

        public void UpgradeLevel()
        {
            if (CurrentLevel < BaseStats.upgradeLevels.Count && CurrentLevel < 5)
            {
                CurrentLevel++;
                Debug.Log($"Upgrading {BaseStats.weaponName} to Level {CurrentLevel}");
                ApplyUpgrades();
            }
        }

        // Getters para stats atuais
        public float GetDamage() => currentDamage;
        public float GetFireRate() => currentFireRate;
        public float GetArea() => currentArea;
        public float GetProjectileSpeed() => currentProjectileSpeed;
        public float GetDuration() => currentDuration;
        public int GetProjectileCount() => currentProjectileCount;

        public bool CanUpgrade() => CurrentLevel < 5;

        public WeaponUpgradeLevel GetNextUpgrade()
        {
            if (CanUpgrade())
                return BaseStats.upgradeLevels[CurrentLevel];
            return null;
        }

        public Vector2 CurrentMovementDirection { get; set; } = Vector2.right; // padrão direita

    }
}
