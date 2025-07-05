using Survivor.Player.Data;

[System.Serializable]
public class RuntimePlayerStats
{
    public int MaxHealth;
    public int MoveSpeed;
    public int DamageReduction;
    public int XPGain;
    public int HealthRegen;
    public int Armor;
    public int Damage;
    public int CritChance;
    public int Area;
    public int AttackSpeed;
    public int Duration;
    public int ProjectileAmount;
    public int CooldownReduction;
    public int RadiusCollect;
    public int Revival;

    private readonly float[] healthValues = { 100f, 120f, 140f, 160f, 180f, 200f };
    private readonly float[] moveSpeedValues = { 1f, 1.5f, 2f, 2.5f, 2.8f, 3f };
    private readonly float[] damageReductionValues = { 0f, 0.05f, 0.1f, 0.15f, 0.2f, 0.25f };
    private readonly float[] damageValues = { 0f, 0.1f, 0.2f, 0.3f, 0.4f, 0.5f };
    private readonly float[] regenValues = { 0f, 0.01f, 0.015f, 0.02f, 0.025f, 0.03f };
    private readonly float[] cooldownReduction = { 0f, 0.05f, 0.1f, 0.15f, 0.2f, 0.25f };
    private readonly float[] xpGainValues = { 0f, 0.1f, 0.15f, 0.2f, 0.25f, 0.3f };
    private readonly float[] areaValues = { 0f, 0.1f, 0.2f, 0.25f, 0.3f, 0.35f };
    private readonly float[] atkSpeedValues = { 0f, 0.05f, 0.1f, 0.15f, 0.2f, 0.25f };
    private readonly float[] durationValues = { 0f, 0.1f, 0.15f, 0.2f, 0.25f, 0.3f };
    private readonly float[] radiusCollectValues = { 0f, 0.5f, 1f, 1.5f, 2f, 2.5f };
    private readonly float[] critChanceValues = { 0f, 0.05f, 0.1f, 0.15f, 0.2f, 0.25f };



    public float GetMaxHealth() => healthValues[MaxHealth];
    public float GetMoveSpeed() => moveSpeedValues[MoveSpeed];
    public float GetDamageReduction() => damageReductionValues[DamageReduction];
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

    public void LoadFrom(PlayerStatsSO baseStats)
    {
        MaxHealth = baseStats.MaxHealth;
        MoveSpeed = baseStats.MoveSpeed;
        DamageReduction = baseStats.DamageReduction;
        XPGain = baseStats.XPGain;
        HealthRegen = baseStats.HealthRegen;
        Damage = baseStats.Damage;
        CritChance = baseStats.CritChance;
        Area = baseStats.Area;
        AttackSpeed = baseStats.AttackSpeed;
        Duration = baseStats.Duration;
        ProjectileAmount = baseStats.ProjectileAmount;
        CooldownReduction = baseStats.CooldownReduction;
        RadiusCollect = baseStats.RadiusCollect;
        Revival = baseStats.Revival;
    }

    public int GetStatLevel(PlayerStatType type)
    {
        return type switch
        {
            PlayerStatType.MaxHealth => MaxHealth,
            PlayerStatType.MoveSpeed => MoveSpeed,
            PlayerStatType.DamageReduction => DamageReduction,
            PlayerStatType.XPGain => XPGain,
            PlayerStatType.HealthRegen => HealthRegen,
            PlayerStatType.Armor => Armor,
            PlayerStatType.Damage => Damage,
            PlayerStatType.CritChance => CritChance,
            PlayerStatType.Area => Area,
            PlayerStatType.AttackSpeed => AttackSpeed,
            PlayerStatType.Duration => Duration,
            PlayerStatType.ProjectileAmount => ProjectileAmount,
            PlayerStatType.CooldownReduction => CooldownReduction,
            PlayerStatType.RadiusCollect => RadiusCollect,
            PlayerStatType.Revival => Revival,
            _ => 0
        };
    }

    public void UpgradeStat(PlayerStatType type)
    {
        switch (type)
        {
            case PlayerStatType.MaxHealth: if (MaxHealth < 5) MaxHealth++; break;
            case PlayerStatType.MoveSpeed: if (MoveSpeed < 5) MoveSpeed++; break;
            case PlayerStatType.DamageReduction: if (DamageReduction < 5) DamageReduction++; break;
            case PlayerStatType.XPGain: if (XPGain < 5) XPGain++; break;
            case PlayerStatType.HealthRegen: if (HealthRegen < 5) HealthRegen++; break;
            case PlayerStatType.Armor: if (Armor < 5) Armor++; break;
            case PlayerStatType.Damage: if (Damage < 5) Damage++; break;
            case PlayerStatType.CritChance: if (CritChance < 5) CritChance++; break;
            case PlayerStatType.Area: if (Area < 5) Area++; break;
            case PlayerStatType.AttackSpeed: if (AttackSpeed < 5) AttackSpeed++; break;
            case PlayerStatType.Duration: if (Duration < 5) Duration++; break;
            case PlayerStatType.ProjectileAmount: if (ProjectileAmount < 5) ProjectileAmount++; break;
            case PlayerStatType.CooldownReduction: if (CooldownReduction < 5) CooldownReduction++; break;
            case PlayerStatType.RadiusCollect: if (RadiusCollect < 5) RadiusCollect++; break;
            case PlayerStatType.Revival: if (Revival < 5) Revival++; break;
        }
    }

}
