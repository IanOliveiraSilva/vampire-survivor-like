using Survivor.Weapons;
using Survivor.Weapons.Data;
using UnityEngine;

public class Orbiter : MonoBehaviour
{
    private Transform pivot;
    private WeaponStatsSO weaponStats;
    private float lifeTime;
    private float currentAngle;
    private float orbitDistance = 1f;

    public void Initialize(Transform _pivot, WeaponStatsSO _weaponStats)
    {
        pivot = _pivot;
        weaponStats = _weaponStats;
        lifeTime = weaponStats.Duration;
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        if(pivot == null)
        {
            Destroy(gameObject);
            return;
        }

        currentAngle += weaponStats.ProjectileSpeed * Time.deltaTime;
        Vector3 offset = new Vector3(Mathf.Cos(currentAngle), Mathf.Sin(currentAngle), 0) * orbitDistance;
        transform.position = pivot.position + offset;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.TakeDamage(weaponStats.Damage);
        }
    }
}
