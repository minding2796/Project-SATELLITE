using System;
using UIScript;
using Unity.VisualScripting;
using UnityEngine;

namespace PlayerScript
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private float speed, jumpForce;
        public int maxJumpCount;
        public static int JumpCount;
        private Rigidbody2D _rigidbody2D;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _rigidbody2D.velocity = new Vector2(MoveButtons.Direction * speed, MoveButtons.TriggerJump ? jumpForce : _rigidbody2D.velocity.y);
            if (!MoveButtons.TriggerJump && Math.Abs(_rigidbody2D.velocity.y - 0) < 0.001 && _rigidbody2D.IsTouchingLayers(LayerMask.GetMask("Ground"))) JumpCount = maxJumpCount;
            MoveButtons.TriggerJump = false;
        }
    }
}
