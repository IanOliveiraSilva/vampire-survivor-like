
using UnityEngine;

namespace Survivor.Enemies.Data
{
    [CreateAssetMenu(fileName = "New Enemy Stats", menuName = "Data/EnemyStats")]
    public class EnemyStatsSO : ScriptableObject
    {
        [Header("Enemy Base Stats")]
        public float MaxHealth = 100f;  
        public float MoveSpeed = 2f;       
        public float contactDamage = 15f;      
        public float attackCooldown = 1.5f;     
        public int xpReward = 5;               

        [Header("Enemy Boss Stats")]
        public bool isBoss = false;
        public float MaxHealthBoss = 500f;      
        public float MoveSpeedBoss = 2.5f;
        public float contactDamageBoss = 35f;
        public float attackCooldownBoss = 1.2f;

        [Header("Assets")]
        public GameObject enemyPrefab;

    }
}
