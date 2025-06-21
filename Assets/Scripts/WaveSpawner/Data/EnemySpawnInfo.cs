using Survivor.Enemies.Data;
using System;

namespace Survivor.WaveSpawner.Data
{
    [System.Serializable]
    public class EnemySpawnInfo
    {
        public EnemyStatsSO enemyStats;
        public int count;
    }
}