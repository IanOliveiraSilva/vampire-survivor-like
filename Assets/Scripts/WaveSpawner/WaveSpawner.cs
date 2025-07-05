using Survivor.WaveSpawner.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survivor.WaveSpawner
{
    public class WaveSpawner : MonoBehaviour
    {
        [SerializeField]
        private List<WaveSO> waves;
        [SerializeField]
        private Transform playerTransform;
        private float spawnRadius = 15f;

        private int currentWaveIndex = 0;

        private void Start()
        {
            if(waves.Count > 0)
            {
                StartCoroutine(SpawnWaves());
            }
        }

        private IEnumerator SpawnWaves()
        {
            while(currentWaveIndex < waves.Count)
            {
                WaveSO currentWave = waves[currentWaveIndex];

                foreach(var spawnInfo in currentWave.EnemiesToSpawn)
                {
                    for(int i = 0; i < spawnInfo.count; i++)
                    {
                        SpawnEnemy(spawnInfo.enemyStats);
                        yield return new WaitForSeconds(currentWave.TimeBetweenSpawns);
                    }
                }

                yield return new WaitForSeconds(currentWave.TimeForNextWave);
                currentWaveIndex++;
            }
        }

        private void SpawnEnemy(Enemies.Data.EnemyStatsSO enemyStats)
        {
            Vector2 spawnPos = GetRandomSpawnPosition();
            Instantiate(enemyStats.enemyPrefab, spawnPos, Quaternion.identity);
        }

        private Vector2 GetRandomSpawnPosition()
        {
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            return (Vector2)playerTransform.position + randomDirection * spawnRadius;
        }
    }
}

