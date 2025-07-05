using Survivor.Weapons;
using Survivor.Weapons.Data;
using UnityEngine;

public class TornadoProjectile : MonoBehaviour
{
    private RuntimeWeaponStats weaponStats;
    private Vector2 moveDirection;

    private float moveSpeed;
    private float rotationSpeed = 360f; // graus por segundo (tornado girando)

    public void Initialize(RuntimeWeaponStats stats, Vector2 direction)
    {
        weaponStats = stats;
        moveDirection = direction.normalized;
        moveSpeed = weaponStats.GetProjectileSpeed();

        Destroy(gameObject, weaponStats.GetDuration());
    }

    private void Update()
    {
        // Movimento para frente
        transform.position += (Vector3)(moveDirection * moveSpeed * Time.deltaTime);

        // Girar o sprite para dar efeito de tornado
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f );

        Debug.Log($"Tornado Projectile at {transform.position} with speed {moveSpeed}");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && other.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.TakeDamage(weaponStats.GetDamage());
        }
    }
}
