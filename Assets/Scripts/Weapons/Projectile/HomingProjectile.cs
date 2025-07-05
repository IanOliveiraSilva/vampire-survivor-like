using Survivor.Weapons;
using Survivor.Weapons.Data;
using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
    private RuntimeWeaponStats weaponStats;
    private Transform target;

    public void Initialize(RuntimeWeaponStats _weaponStats, Transform _target)
    {
        weaponStats = _weaponStats;
        target = _target;

        Destroy(gameObject, weaponStats.GetDuration());
    }

    private void Update()
    {
        if (target == null || !target.gameObject.activeInHierarchy)
        {
            Destroy(gameObject);
            return;
        }

        Vector2 directionToTarget = (target.position - transform.position).normalized;

        // Girar suavemente na direção do alvo
        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, -angle);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);

        // Movimento direto na direção do alvo
        transform.position += (Vector3)(directionToTarget * weaponStats.GetProjectileSpeed() * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.TakeDamage(weaponStats.GetDamage());
            Destroy(gameObject);
        }
    }
}
