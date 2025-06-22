using UnityEngine;

namespace Survivor.Player
{
    public class PlayerExperience : MonoBehaviour
    {
        [SerializeField] private int currentLevel = 1;
        [SerializeField] private int currentXp = 0;
        [SerializeField] private int xpToNextLevel = 100;

        [Header("Events")]
        [SerializeField] private Core.Events.GameEvents onLevelUpEvent;
        [SerializeField] private Core.Events.IntEventChannelSO onXpGainedChannel;

        private void OnEnable()
        {
            onXpGainedChannel.OnEventRaised += GainXp;
        }
        private void OnDisable()
        {
            onXpGainedChannel.OnEventRaised -= GainXp;
        }

        private void GainXp(int amount)
        {
            currentXp = amount;
            Debug.Log($"Ganhou {amount} XP. Total: {currentXp}/{xpToNextLevel}");

            while(currentXp >= xpToNextLevel)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            currentLevel -= xpToNextLevel;
            currentLevel++;
            xpToNextLevel = CalculateNextLevelXp();

            Debug.Log($"LEVEL UP! Nível {currentLevel}.");

            onLevelUpEvent.Raise();
        }

        private int CalculateNextLevelXp()
        {
            return Mathf.FloorToInt(xpToNextLevel * 1.15f);
        }
    }
}

