using Survivor.Weapons;
using Survivor.Weapons.Data;
using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
    private WeaponStatsSO weaponStats;
    private Transform target;

    public void Initialize(WeaponStatsSO _weaponStats, Transform _target)
    {
        weaponStats = _weaponStats;
        target = _target;

        Destroy(gameObject, weaponStats.Duration);
    }

    private void Update()
    {
        if (target == null || !target.gameObject.activeInHierarchy)
        {
            Destroy(gameObject);
            return;
        }

        Vector2 directionToTarget = (target.position - transform.position).normalized;

        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);

        float rotationSpeed = 10f;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.Translate(Vector2.right * weaponStats.ProjectileSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.TakeDamage(weaponStats.Damage);
            Destroy(gameObject);
        }
    }
}