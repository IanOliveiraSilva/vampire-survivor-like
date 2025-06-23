using UnityEngine;
using UnityEngine.Events;

namespace Survivor.Core.Events
{
    [CreateAssetMenu(menuName = "Events/Float Event Channel")]
    public class FloatEventChannelSO : ScriptableObject
    {
        public UnityAction<float> OnEventRaised;

        public void Raise(float value)
        {
            OnEventRaised?.Invoke(value);
        }
    }
}
