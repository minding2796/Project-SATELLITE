using UnityEngine;
using UnityEngine.UI;

namespace UIScript
{
    public class ScreenTouchExecutor : MonoBehaviour
    {
        public Button.ButtonClickedEvent onScreenTouch;
        public static bool DisableTouch;
        private void Update()
        {
            if (!DisableTouch && Input.GetMouseButtonDown(0)) onScreenTouch.Invoke();
        }
    }
}
