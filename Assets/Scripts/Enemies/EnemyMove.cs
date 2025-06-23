using UnityEngine;

namespace Survivor.Enemies
{
    public class EnemyMove : MonoBehaviour
    {
        private float speed = 2f;

        private Transform playerTransform;
        private Rigidbody2D rb2D;

        private void Awake()
        {
            rb2D = GetComponent<Rigidbody2D>();
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerTransform = player.transform;
            }
        }

        private void FixedUpdate()
        {
            if (playerTransform == null) return;

            Vector2 direction = (playerTransform.position - transform.position).normalized;
            rb2D.linearVelocity = direction * speed;
        }
        
        public void SetSpeed(float _speed)
        {
            speed = _speed;
        }
    }
}
