using Survivor.Weapons;
using UnityEngine;

public class AcidPool : MonoBehaviour
{
    private RuntimeWeaponStats weaponStats;
    private float duration;
    private float damagePerTick = 1f;
    private float tickInterval = 0.5f;

    private void Start()
    {
        InvokeRepeating(nameof(ApplyDamage), 0f, tickInterval);
        Destroy(gameObject, duration);
    }

    public void Initialize(RuntimeWeaponStats _stats)
    {
        weaponStats = _stats;
        duration = weaponStats.GetDuration();
        damagePerTick = weaponStats.GetDamage();
    }

    private void ApplyDamage()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, weaponStats.GetArea(), LayerMask.GetMask("Enemy"));

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(damagePerTick);
            }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, weaponStats != null ? weaponStats.GetArea() : 1f);
    }
#endif
}
