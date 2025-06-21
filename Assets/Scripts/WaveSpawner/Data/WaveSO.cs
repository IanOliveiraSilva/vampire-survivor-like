using UnityEngine;
using System.Collections.Generic;

namespace Survivor.WaveSpawner.Data
{
    [CreateAssetMenu(fileName = "New Wave Stats", menuName = "Data/WaveStats")]
    public class WaveSO : ScriptableObject
    {
        public List<EnemySpawnInfo> EnemiesToSpawn;
        public float TimeBetweenSpawns = 0.5f;
        public float TimeForNextWave = 10f;
    }
}

