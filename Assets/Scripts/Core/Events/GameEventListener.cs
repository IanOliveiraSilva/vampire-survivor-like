using UnityEngine;
using UnityEngine.Events;

namespace Survivor.Core.Events
{
    public class GameEventListener : MonoBehaviour
    {

        public GameEvents Event;


        public UnityEvent Response;

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public void OnEventRaised()
        {
            Response.Invoke();
        }
    }
}