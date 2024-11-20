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
        public static bool IsGravityReversed { get; private set; }
        public static Vector3 EulerAngles;
        private Rigidbody2D _rigidbody2D;

        private void Start()
        {
            EulerAngles = transform.eulerAngles;
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            IsGravityReversed = _rigidbody2D.IsTouchingLayers(LayerMask.GetMask("ReverseGravity"));
            _rigidbody2D.gravityScale = IsGravityReversed ? -1 : 1;
            EulerAngles.x = IsGravityReversed ? 180 : 0;
            EulerAngles.y = MoveButtons.Direction != 0 ? (MoveButtons.Direction - 1) * 90 : EulerAngles.y;
            transform.rotation = Quaternion.Euler(EulerAngles);
            _rigidbody2D.velocity = new Vector2(MoveButtons.Direction * speed * (_rigidbody2D.IsTouchingLayers(LayerMask.GetMask("Accelerator")) ? 2f : 1f), MoveButtons.TriggerJump ? jumpForce * (IsGravityReversed ? -1 : 1) : _rigidbody2D.velocity.y);
            if (!MoveButtons.TriggerJump && _rigidbody2D.velocity.y < jumpForce * 0.75f && _rigidbody2D.IsTouchingLayers(LayerMask.GetMask("Ground"))) JumpCount = maxJumpCount;
            MoveButtons.TriggerJump = false;
        }
    }
}
