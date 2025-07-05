using UnityEngine;
using Survivor.Player.Data;
namespace Survivor.Player
{
    public class PlayerMove : MonoBehaviour
    {
        private Rigidbody2D rb2D;
        private PlayerControls playerControls;
        private Vector2 moveInput;

        [SerializeField] private float speed;

        private PlayerAnimation playerAnimation;

        private void Awake()
        {
            rb2D = GetComponent<Rigidbody2D>();
            playerAnimation = GetComponent<PlayerAnimation>();
            playerControls = new PlayerControls();
        }

        private void OnEnable()
        {
            playerControls.Player.Enable();
        }

        private void OnDisable()
        {
            playerControls.Player.Disable();
        }

        private void Update()
        {
            moveInput = playerControls.Player.Move.ReadValue<Vector2>();
            playerAnimation.UpdateAnimation(moveInput.normalized);

        }

        private void FixedUpdate()
        {
            rb2D.linearVelocity = moveInput.normalized * speed;
        }

        public void SetSpeed(float _speed)
        {
            speed = _speed;
        }
        public Vector2 CurrentMoveDirection => moveInput.normalized;

    }
}

