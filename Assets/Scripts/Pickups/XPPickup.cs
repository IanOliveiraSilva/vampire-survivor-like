using UnityEngine;

namespace Survivor.Pickup
{
    public class XPPickup : MonoBehaviour
    {
        public int xpAmount = 5;

        [SerializeField]
        private Core.Events.IntEventChannelSO onXpGainedChannel;

        public void Initialize(int amout)
        {
            xpAmount = amout;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                onXpGainedChannel?.Raise(xpAmount);

                Destroy(gameObject);
            }
        }
    }

}
