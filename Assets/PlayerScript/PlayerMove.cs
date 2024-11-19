using UIScript;
using UnityEngine;

namespace PlayerScript
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private float speed, jumpForce;
        public int maxJumpCount;
        public static int JumpCount;
        public static PlayerNpcDetection CurrentNpcDetection;
        private Rigidbody2D _rigidbody2D;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (MoveButtons.Direction != 0) transform.rotation = Quaternion.Euler(0, (MoveButtons.Direction - 1) * 90, 0);
            _rigidbody2D.velocity = new Vector2(MoveButtons.Direction * speed * (_rigidbody2D.IsTouchingLayers(LayerMask.GetMask("Accelerator")) ? 2f : 1f), MoveButtons.TriggerJump ? jumpForce : _rigidbody2D.velocity.y);
            if (!MoveButtons.TriggerJump && _rigidbody2D.velocity.y < jumpForce * 0.75f && _rigidbody2D.IsTouchingLayers(LayerMask.GetMask("Ground"))) JumpCount = maxJumpCount;
            MoveButtons.TriggerJump = false;
        }
    }
}
