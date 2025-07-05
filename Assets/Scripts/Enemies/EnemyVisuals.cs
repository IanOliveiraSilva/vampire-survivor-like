using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class EnemyVisuals : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        HandleSpriteFlipping();
    }


    private void HandleSpriteFlipping()
    {
        // Pega a velocidade horizontal do inimigo.
        float horizontalVelocity = rb2D.linearVelocity.x;

        // Define um pequeno limiar para evitar "tremidas" quando o inimigo está quase parado.
        const float threshold = 0.01f;

        // Se a velocidade horizontal for para a direita...
        if (horizontalVelocity > threshold)
        {
            // Garante que o sprite não esteja espelhado (olhando para a direita).
            spriteRenderer.flipX = false;
        }
        // Se a velocidade horizontal for para a esquerda...
        else if (horizontalVelocity < -threshold)
        {
            // Espelha o sprite (olhando para a esquerda).
            spriteRenderer.flipX = true;
        }
        // Se a velocidade for praticamente zero, não fazemos nada,
        // mantendo a última direção que ele estava olhando.
    }
}