using Survivor.Weapons.Data;
using UnityEngine;

[CreateAssetMenu(fileName = "GarlicStrategy", menuName = "Strategies/Aura Strategy")]
public class AuraStrategy : WeaponStrategy
{
    private bool hasSpawned = false;

    public override void Attack(Transform attacker, WeaponStatsSO stats, int projectileLayer)
    {
        if (hasSpawned) return;

        for (int i = 0; i < stats.ProjectileCount; i++)
        {
            // O prefab precisa ter o seu script "Aura.cs".
            GameObject cloveInstance = Instantiate(stats.ProjectilePrefab, attacker);

            // Usaremos ProjectileSpeed para definir a distância da aura.
            float distance = stats.ProjectileSpeed;
            float angle = (360f / stats.ProjectileCount) * i;
            Vector3 position = Quaternion.Euler(0, 0, angle) * Vector3.right * distance;
            cloveInstance.transform.localPosition = position;

            // Inicializa a aura com os status da arma.
            if (cloveInstance.TryGetComponent<Aura>(out var aura))
            {
                aura.Initialize(stats);
            }
        }
        hasSpawned = true;
    }

    private void OnDisable()
    {
        hasSpawned = false;
    }
}