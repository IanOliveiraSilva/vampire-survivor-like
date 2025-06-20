using UnityEngine;
using Survivor.Core.Data;

namespace Survivor.Player.Data
{
    [CreateAssetMenu(fileName="New Player Stats", menuName="Data/PlayerStats")]
    public class PlayerStatsSO : BaseStatsSO
    {
        public float DamageReduction = 0f;
        public float XPGainModifier = 1f;
    }
}


