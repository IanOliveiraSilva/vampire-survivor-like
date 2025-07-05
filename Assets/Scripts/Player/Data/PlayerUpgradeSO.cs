using UnityEngine;

namespace Survivor.Player.Data
{
    [CreateAssetMenu(fileName = "New Player Upgrade", menuName = "Data/PlayerUpgrade")]
    public class PlayerUpgradeSO : ScriptableObject
    {
        public PlayerStatType statType;
        public Sprite icon;
        public string statName;

        [TextArea]
        public string[] levelDescriptions = new string[5]; // Descrições para níveis 1 a 5
    }
}
