using Survivor.Weapons;
using Survivor.Weapons.Data;
using UnityEngine;

[CreateAssetMenu(fileName = "DirectionalProjectileStrategy", menuName = "Strategies/DirectionalProjectileStrategy")]
public class DirectionalStrategy : WeaponStrategy
{
    public override void Attack(Transform attacker, RuntimeWeaponStats stats, int projectileLayer)
    {
        if (!string.IsNullOrEmpty(stats.BaseStats.attackSoundName))
        {
            AudioManager.Instance.PlaySFX(stats.BaseStats.attackSoundName);
        }

        Vector2 moveDirection = stats.CurrentMovementDirection;
        if (moveDirection == Vector2.zero)
        {
            // Caso o jogador esteja parado, atira para frente (direita) por padrão
            moveDirection = Vector2.right;
        }

        int projectileCount = stats.GetProjectileCount();
        float spreadAngle = 10f; // Pode ajustar para dar uma variação entre projéteis

        for (int i = 0; i < projectileCount; i++)
        {
            // Calcular ângulo de dispersão
            float angleOffset = spreadAngle * (i - (projectileCount - 1) / 2f);
            Vector2 rotatedDirection = Quaternion.Euler(0f, 0f, angleOffset) * moveDirection;
            float angle = Mathf.Atan2(rotatedDirection.y, rotatedDirection.x) * Mathf.Rad2Deg;

            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
            GameObject projectileInstance = Instantiate(stats.BaseStats.projectilePrefab, attacker.position, rotation);
            projectileInstance.layer = projectileLayer;

            if (projectileInstance.TryGetComponent<DirectionalProjectile>(out var directionalProjectile))
            {
                directionalProjectile.Initialize(stats, rotatedDirection.normalized);
            }
        }
    }
}
