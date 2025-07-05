using Survivor.Weapons;
using Survivor.Weapons.Data;
using UnityEngine;

[CreateAssetMenu(fileName = "AuraStrategy", menuName = "Strategies/AuraStrategy")]
public class AuraStrategy : WeaponStrategy
{
    private GameObject auraInstance;

    public override void Attack(Transform attacker, RuntimeWeaponStats stats, int projectileLayer)
    {
        if (auraInstance != null) return;  // Já tem aura ativa, não cria outra

        auraInstance = Instantiate(stats.BaseStats.projectilePrefab, attacker.position, Quaternion.identity);
        auraInstance.layer = projectileLayer;
        auraInstance.transform.SetParent(attacker);  // Fixa no jogador para seguir ele

        if (auraInstance.TryGetComponent<Aura>(out var aura))
        {
            aura.Initialize(stats);
            aura.OnAuraDestroyed += HandleAuraDestroyed;
        }
    }

    private void HandleAuraDestroyed()
    {
        auraInstance = null;
    }
}
