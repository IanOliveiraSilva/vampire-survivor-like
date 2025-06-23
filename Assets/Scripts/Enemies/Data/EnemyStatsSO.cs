using Survivor.Core.Data;
using UnityEngine;

namespace Survivor.Enemies.Data
{
    [CreateAssetMenu(fileName = "New Enemy Stats", menuName = "Data/EnemyStats")]
    public class EnemyStatsSO : BaseStatsSO
    {
        [Header("Enemy Stats")]
        public float contactDamage = 10f;
        public float attackCooldown = 1f;
        public int xpReward = 5;

        [Header("Assets")]
        public GameObject enemyPrefab;
    }
}
