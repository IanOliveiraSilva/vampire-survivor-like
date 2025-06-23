using UnityEngine;
using UnityEngine.Events;


namespace Survivor.Core.Events
{
    [CreateAssetMenu(menuName = "Events/Int Event Channel")]
    public class IntEventChannelSO : ScriptableObject
    {
        public UnityAction<int> OnEventRaised;

        public void Raise(int value)
        {
            OnEventRaised?.Invoke(value);
        }
    }
}

