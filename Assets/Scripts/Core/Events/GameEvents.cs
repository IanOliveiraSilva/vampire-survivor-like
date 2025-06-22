using System.Collections.Generic;
using UnityEngine;

namespace Survivor.Core.Events
{
    [CreateAssetMenu(menuName = "Events/Game Event")]
    public class GameEvents : ScriptableObject
    {
        private readonly List<GameEventListener> _listeners = new List<GameEventListener>();

        public void Raise()
        {
            // Percorremos a lista de trás para frente para evitar problemas
            // se um listener for removido durante a iteração.
            for (int i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].OnEventRaised();
            }
        }

        public void RegisterListener(GameEventListener listener)
        {
            if (!_listeners.Contains(listener))
                _listeners.Add(listener);
        }

        public void UnregisterListener(GameEventListener listener)
        {
            if (_listeners.Contains(listener))
                _listeners.Remove(listener);
        }
    }
}