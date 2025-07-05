using UnityEngine;

namespace Survivor.Pickup
{
    public class XPPickup : MonoBehaviour
    {
        [Header("XP Settings")]
        public int xpAmount = 5;

        [Header("Attraction Settings")]
        [SerializeField] private float moveSpeed = 5f;

        [Header("Events")]
        [SerializeField] private Core.Events.FloatEventChannelSO onXpGainedChannel;

        private Transform playerTransform;
        private Player.PlayerController playerController;

        public void Initialize(int amount)
        {
            xpAmount = amount;
        }

        private void Start()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerTransform = player.transform;
                playerController = player.GetComponent<Player.PlayerController>();
            }
        }

        private void Update()
        {
            if (playerTransform == null || playerController == null) return;

            float attractionRadius = playerController.GetRadiusCollect();

            float distance = Vector2.Distance(transform.position, playerTransform.position);
            if (distance < attractionRadius)
            {
                Vector2 direction = (playerTransform.position - transform.position).normalized;
                transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                AudioManager.Instance.PlaySFX("xp_collect");
                onXpGainedChannel?.Raise(xpAmount);
                Destroy(gameObject);
            }
        }
    }
}
