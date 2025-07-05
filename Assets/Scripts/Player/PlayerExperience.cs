using UnityEngine;

namespace Survivor.Player
{
    public class PlayerExperience : MonoBehaviour
    {
        [SerializeField] private float currentLevel = 1;
        [SerializeField] private float currentXp = 0;
        [SerializeField] private float xpToNextLevel = 100;

        [SerializeField] private float XpGainModifier;

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
            float xpGained = amount * (1f + XpGainModifier);
            currentXp += xpGained;

            while (currentXp >= xpToNextLevel)
            {
                LevelUp();
            }

            UpdateXpBar();
        }


        private void LevelUp()
        {
            AudioManager.Instance.PlaySFX("level_up");

            currentXp -= xpToNextLevel;
            currentLevel++;
            xpToNextLevel = CalculateNextLevelXp();

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
        }

        public void SetXpGainModifier(float amount) 
        {
            XpGainModifier = amount;
        }
    }
}

