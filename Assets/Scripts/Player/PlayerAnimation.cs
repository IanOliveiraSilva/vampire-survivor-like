using UnityEngine;

namespace Survivor.Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void UpdateAnimation(Vector2 direction)
        {
            bool isMoving = direction.sqrMagnitude > 0.01f;

            animator.SetBool("IsMoving", isMoving);

            if (isMoving)
            {
                // Define a direção de movimento
                animator.SetFloat("MoveX", direction.x);
                animator.SetFloat("MoveY", direction.y);
            }
        }


    }
}
