using UnityEngine;

namespace Survivor.Player.Data
{
    [CreateAssetMenu(fileName = "New Player Stats", menuName = "Data/PlayerStats")]
    public class PlayerStatsSO : ScriptableObject
    {
        [Header("Upgrade Levels (0-5)")]
        [Range(0, 5)] public int MaxHealth = 0;
        [Range(0, 5)] public int MoveSpeed = 0;
        [Range(0, 5)] public int DamageReduction = 0;
        [Range(0, 5)] public int XPGain = 0;
        [Range(0, 5)] public int HealthRegen = 0;
        [Range(0, 5)] public int Damage = 0;
        [Range(0, 5)] public int CritChance = 0;
        [Range(0, 5)] public int Area = 0;
        [Range(0, 5)] public int AttackSpeed = 0;
        [Range(0, 5)] public int Duration = 0;
        [Range(0, 5)] public int ProjectileAmount = 0;
        [Range(0, 5)] public int CooldownReduction = 0;
        [Range(0, 5)] public int RadiusCollect = 0;
        [Range(0, 5)] public int Revival = 0;

        private readonly float[] healthValues = { 100f, 120f, 140f, 160f, 180f, 200f };
        private readonly float[] moveSpeedValues = { 1f, 1.5f, 2f, 2.5f, 2.8f, 3f };
        private readonly float[] armorValues = { 0f, 0.05f, 0.1f, 0.15f, 0.2f, 0.25f };
        private readonly float[] damageValues = { 0f, 0.1f, 0.2f, 0.3f, 0.4f, 0.5f };
        private readonly float[] regenValues = { 0f, 0.01f, 0.012f, 0.016f, 0.02f, 0.025f };
        private readonly float[] cooldownReduction = { 0f, 0.05f, 0.1f, 0.15f, 0.2f, 0.25f };
        private readonly float[] xpGainValues = { 0f, 0.1f, 0.15f, 0.2f, 0.25f, 0.3f };
        private readonly float[] areaValues = { 0f, 0.1f, 0.2f, 0.25f, 0.3f, 0.35f };
        private readonly float[] atkSpeedValues = { 0f, 0.05f, 0.1f, 0.15f, 0.2f, 0.25f };
        private readonly float[] durationValues = { 0f, 0.1f, 0.15f, 0.2f, 0.25f, 0.3f };
        private readonly float[] radiusCollectValues = { 0f, 0.5f, 1f, 1.5f, 2f, 2.5f };
        private readonly float[] critChanceValues = { 0f, 0.05f, 0.1f, 0.15f, 0.2f, 0.25f };


        public float GetMaxHealth() => healthValues[MaxHealth];
        public float GetMoveSpeed() => moveSpeedValues[MoveSpeed];
        public float GetDamageReduction() => armorValues[DamageReduction];
        public float GetDamageModifier() => damageValues[Damage];
        public float GetHealthRegen() => regenValues[HealthRegen];
        public float GetCooldownReduction() => cooldownReduction[CooldownReduction];
        public float GetXPModifier() => xpGainValues[XPGain];
        public float GetAreaModifier() => areaValues[Area];
        public float GetAttackSpeedModifier() => atkSpeedValues[AttackSpeed];
        public float GetDurationModifier() => durationValues[Duration];
        public float GetRadiusCollectModifier() => radiusCollectValues[RadiusCollect];
        public float GetCritChanceModifier() => critChanceValues[CritChance];


        public int GetProjectileAmount() => ProjectileAmount;
        public int GetRevivalCount() => Revival;
    }
}
