using UnityEngine;

namespace Survivor.Core.Data
{
    public abstract class BaseStatsSO : ScriptableObject
    {
        [Header("Visuals")]
        public Sprite Sprite;

        [Header("Stats")]
        public float MaxHealth = 100f;
        public float MoveSpeed = 5f;
    }
}