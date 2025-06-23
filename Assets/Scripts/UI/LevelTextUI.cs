using Survivor.Player;
using TMPro;
using UnityEngine;

namespace Survivor.UI
{
    public class LevelTextUI : MonoBehaviour
    {
        private TextMeshProUGUI levelText;

        [SerializeField] private Core.Events.FloatEventChannelSO onLevelUpEvent;

        private void OnEnable()
        {
            onLevelUpEvent.OnEventRaised += UpdateText;
        }
        private void OnDisable()
        {
            onLevelUpEvent.OnEventRaised -= UpdateText;
        }

        private void Awake()
        {
            levelText = GetComponent<TextMeshProUGUI>();
        }

        private void UpdateText(float currentLevel)
        {
            levelText.text = $"{currentLevel}";
        }
    }

}
