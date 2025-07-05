using UnityEngine;
using Survivor.Player.Data;

namespace Survivor.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private PlayerStatsSO playerStats;

        private PlayerMove playerMove;
        private PlayerHealth playerHealth;
        private PlayerExperience playerExperience;

        [SerializeField] private RuntimePlayerStats playerStatsOnRun;

        private void Awake()
        {
            playerMove = GetComponent<PlayerMove>();
            playerHealth = GetComponent<PlayerHealth>();
            playerExperience = GetComponent<PlayerExperience>();

            playerStatsOnRun = new RuntimePlayerStats();
            playerStatsOnRun.LoadFrom(playerStats);

            InitializeOnce(); // Só configura a vida inicial
        }

        private void Update()
        {
            UpdateStats(); // Chamado todo frame
        }

        private void InitializeOnce()
        {
            playerHealth.SetMaxHealth(playerStatsOnRun.GetMaxHealth());
        }

        private void UpdateStats()
        {
            if (playerStats == null) return;

            playerMove.SetSpeed(playerStatsOnRun.GetMoveSpeed());
            playerHealth.SetArmor(playerStatsOnRun.GetDamageReduction());
            playerHealth.SetHealthRegen(playerStatsOnRun.GetHealthRegen());
            playerExperience.SetXpGainModifier(playerStatsOnRun.GetXPModifier());
        }

        public RuntimePlayerStats GetRuntimePlayerStats()
        {
            return playerStatsOnRun;
        }

        public float GetRadiusCollect()
        {
            return playerStatsOnRun.GetRadiusCollectModifier();
        }

        private void OnDrawGizmosSelected()
        {
#if UNITY_EDITOR
            Gizmos.color = Color.cyan;

            // Se o jogo estiver em Play Mode, usa o valor dinâmico do runtime
            float radius = Application.isPlaying && playerStatsOnRun != null
                ? playerStatsOnRun.GetRadiusCollectModifier()
                : 2.5f; // valor de fallback para o editor

            Gizmos.DrawWireSphere(transform.position, radius);
#endif
        }

    }
}

