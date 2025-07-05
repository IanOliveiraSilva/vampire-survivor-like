using Survivor.Weapons;
using Survivor.Weapons.Data;
using UnityEngine;

public class AxeProjectile : MonoBehaviour
{
    private RuntimeWeaponStats weaponStats;
    private float lifetime;
    private float rotationSpeed = 720f; // graus por segundo
    private Vector2 velocity;
    private float gravity = -9.8f;
    private float horizontalDirection;

    public void Initialize(RuntimeWeaponStats stats)
    {
        weaponStats = stats;
        lifetime = stats.GetDuration();
        Destroy(gameObject, lifetime);

        // Velocidade inicial para cima + direção lateral aleatória (esquerda ou direita)
        float initialUpwardVelocity = 5f;
        float horizontalSpeed = 2f;

        horizontalDirection = Random.value < 0.5f ? -1f : 1f; // esquerda ou direita
        velocity = new Vector2(horizontalSpeed * horizontalDirection, initialUpwardVelocity);
    }

    private void Update()
    {
        // Atualiza a rotação
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        // Aplica física simples: gravidade e movimento
        velocity.y += gravity * Time.deltaTime;

        transform.position += (Vector3)(velocity * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.TakeDamage(weaponStats.GetDamage());
            // Opcional: destruir após impacto
            Destroy(gameObject);
        }
    }
}
