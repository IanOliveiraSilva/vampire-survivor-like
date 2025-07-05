using Survivor.Weapons;
using Survivor.Weapons.Data;
using UnityEngine;

public class DirectionalProjectile : MonoBehaviour
{
    private RuntimeWeaponStats weaponStats;
    private Vector2 direction;

    public void Initialize(RuntimeWeaponStats _weaponStats, Vector2 _direction)
    {
        weaponStats = _weaponStats;
        direction = _direction.normalized;

        Destroy(gameObject, weaponStats.GetDuration());
    }

    private void Update()
    {
        transform.position += (Vector3)(direction * weaponStats.GetProjectileSpeed() * Time.deltaTime);
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
