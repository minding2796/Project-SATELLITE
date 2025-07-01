using System;
using PlayerScript;
using UnityEngine;

namespace UIScript
{
    public class MoveButtons : MonoBehaviour
    {
        private static bool _isLeftButtonDown;
        private static bool _isRightButtonDown;
        public static float Direction
        {
            get
            {
                var axis = Input.GetAxis("Horizontal");
                if (axis != 0) return axis;
                return _isLeftButtonDown switch
                {
                    true when _isRightButtonDown => 0,
                    true => -1,
                    _ => _isRightButtonDown ? 1 : 0
                };
            }
        }

        public static bool TriggerJump;

        private void Start()
        {
            _isLeftButtonDown = false;
            _isRightButtonDown = false;
        }

        private void FixedUpdate()
        {
            if (Input.GetAxis("Jump") == 0) return;
            if (PlayerMove.JumpCount < 1) return;
            TriggerJump = true;
            PlayerMove.JumpCount--;
        }

        public void LeftButtonDown()
        {
            _isLeftButtonDown = true;
        }

        public void LeftButtonUp()
        {
            _isLeftButtonDown = false;
        }

        public void RightButtonDown()
        {
            _isRightButtonDown = true;
        }

        public void RightButtonUp()
        {
            _isRightButtonDown = false;
        }

        public void JumpButton()
        {
            if (PlayerMove.JumpCount < 1) return;
            TriggerJump = true;
            PlayerMove.JumpCount--;
        }
    }
}
