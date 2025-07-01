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
        public bool OnGround =>
            !MoveButtons.TriggerJump &&
            ((IsGravityReversed
                 ? _rigidbody2D.linearVelocity.y > jumpForce * -0.75f
                 : _rigidbody2D.linearVelocity.y < jumpForce * 0.75f) &&
             _rigidbody2D.IsTouchingLayers(LayerMask.GetMask("Ground")) || _rigidbody2D.linearVelocity.y == 0);
        public bool IsAccelerating => _rigidbody2D.IsTouchingLayers(LayerMask.GetMask("Accelerator"));
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
            _rigidbody2D.linearVelocity = new Vector2(
                MoveButtons.Direction * speed * (IsAccelerating ? 2 : 1),
                MoveButtons.TriggerJump ? jumpForce * (IsGravityReversed ? -1 : 1) : _rigidbody2D.linearVelocity.y);
            if (OnGround) JumpCount = maxJumpCount;
            else if (JumpCount == maxJumpCount) JumpCount = maxJumpCount - 1;
            MoveButtons.TriggerJump = false;
        }
    }
}
