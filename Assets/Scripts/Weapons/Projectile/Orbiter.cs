using Survivor.Weapons;
using Survivor.Weapons.Data;
using UnityEngine;
using System;

public class Orbiter : MonoBehaviour
{
    private Transform pivot;
    private RuntimeWeaponStats weaponStats;
    private float lifeTime;
    private float currentAngle;
    private float orbitDistance = 1f;

    public event Action<Orbiter> OnOrbiterDestroyed;

    public void Initialize(Transform _pivot, RuntimeWeaponStats _weaponStats, float startAngle)
    {
        pivot = _pivot;
        weaponStats = _weaponStats;
        lifeTime = weaponStats.GetDuration();
        currentAngle = startAngle * Mathf.Deg2Rad;
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        if (pivot == null)
        {
            DestroySelf();
            return;
        }

        currentAngle += weaponStats.GetProjectileSpeed() * Time.deltaTime;

        Vector3 offset = new Vector3(Mathf.Cos(currentAngle), Mathf.Sin(currentAngle), 0) * orbitDistance;
        transform.position = pivot.position + offset;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.TakeDamage(weaponStats.GetDamage());
        }
    }

    private void OnDestroy()
    {
        OnOrbiterDestroyed?.Invoke(this);
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
