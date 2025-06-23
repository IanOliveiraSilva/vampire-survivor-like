using UnityEngine;

namespace Survivor.Player
{
    public class PlayerExperience : MonoBehaviour
    {
        [SerializeField] private float currentLevel = 1;
        [SerializeField] private float currentXp = 0;
        [SerializeField] private float xpToNextLevel = 100;

        [Header("Events")]
        [SerializeField] private Core.Events.FloatEventChannelSO onLevelUpEvent;
        [SerializeField] private Core.Events.FloatEventChannelSO onXpGainedChannel;
        [SerializeField] private Core.Events.FloatEventChannelSO onXpUpdatedChannel;

        private void Start()
        {
            UpdateXpBar();
        }

        private void OnEnable()
        {
            onXpGainedChannel.OnEventRaised += GainXp;
        }
        private void OnDisable()
        {
            onXpGainedChannel.OnEventRaised -= GainXp;
        }

        private void GainXp(float amount)
        {
            currentXp += amount;
            Debug.Log($"Ganhou {amount} XP. Total: {currentXp}/{xpToNextLevel}");

            while (currentXp >= xpToNextLevel)
            {
                LevelUp();
            }

            UpdateXpBar();
        }

        private void LevelUp()
        {
            currentXp -= xpToNextLevel;
            currentLevel++;
            xpToNextLevel = CalculateNextLevelXp();

            Debug.Log($"LEVEL UP! Nível {currentLevel}.");

            onLevelUpEvent.Raise(currentLevel);
        }

        private int CalculateNextLevelXp()
        {
            return Mathf.FloorToInt(xpToNextLevel * 1.15f);
        }

        private void UpdateXpBar()
        {
            float normalizedXp = currentXp / xpToNextLevel;
            onXpUpdatedChannel.Raise(normalizedXp);
            Debug.Log($"XP Atualizado: {normalizedXp * 100}%");
        }
    }
}

