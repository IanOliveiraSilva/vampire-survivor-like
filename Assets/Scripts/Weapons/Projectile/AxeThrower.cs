using Survivor.Weapons;
using System.Collections;
using UnityEngine;

public class AxeThrower : MonoBehaviour
{
    public void StartThrowing(int count, GameObject prefab, RuntimeWeaponStats stats, int projectileLayer, Vector3 position)
    {
        StartCoroutine(ThrowAxesCoroutine(count, prefab, stats, projectileLayer, position));
    }

    private IEnumerator ThrowAxesCoroutine(int count, GameObject prefab, RuntimeWeaponStats stats, int projectileLayer, Vector3 basePosition)
    {
        for (int i = 0; i < count; i++)
        {
            // Direção aleatória: esquerda ou direita
            float directionX = Random.value < 0.5f ? -1f : 1f;
            float offsetX = directionX * Random.Range(0.3f, 0.7f);
            Vector3 spawnPos = basePosition + new Vector3(offsetX, 0.5f, 0); // cima + deslocamento lateral
            Quaternion rotation = Quaternion.identity;

            GameObject axe = Instantiate(prefab, spawnPos, rotation);
            axe.layer = projectileLayer;

            if (axe.TryGetComponent<AxeProjectile>(out var axeProjectile))
            {
                axeProjectile.Initialize(stats);
            }

            yield return new WaitForSeconds(0.1f); // delay entre os machados
        }

        Destroy(gameObject); // limpa o helper após execução
    }
}
