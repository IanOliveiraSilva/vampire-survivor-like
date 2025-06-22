using UnityEngine;
using Survivor.Player.Data;
namespace Survivor.Player
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField]
        private PlayerStatsSO playerStats;

        private Rigidbody2D rb2D;
        private PlayerControls playerControls;
        private Vector2 moveInput;

        private void Awake()
        {
            rb2D = GetComponent<Rigidbody2D>();
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
            Debug.Log(moveInput.normalized);
        }

        private void FixedUpdate()
        {
            rb2D.linearVelocity = moveInput.normalized * playerStats.MoveSpeed;
        }
    }
}

