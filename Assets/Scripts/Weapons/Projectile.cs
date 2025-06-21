using UnityEngine;

namespace Survivor.Weapons
{
    public class Projectile : MonoBehaviour
    {
        private float speed;
        private float damage;
        private Vector2 direction;
        private float lifeSpan = 5f; // Projéteis se destroem após 5 segundos

        public void Initialize(float _speed, float _damage, Vector2 _direction)
        {
            speed = _speed;
            damage = _damage;
            direction = _direction;

            // Destroi o projétil após o tempo de vida
            Destroy(gameObject, lifeSpan);
        }

        private void Update()
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Usaremos uma interface para desacoplar o dano
            if (other.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(damage);
                Destroy(gameObject); // Destroi o projétil ao atingir um alvo
            }
        }
    }
    // Interface para qualquer coisa que possa receber dano
    public interface IDamageable
    {
        void TakeDamage(float amount);
    }
}

