using UnityEngine;
using UnityEngine.UI;

namespace ManagerScript
{
    public class ColliderManager : MonoBehaviour
    {
        public Button.ButtonClickedEvent onTriggerEnter2D;
        public Button.ButtonClickedEvent onceTriggerEnter2D;

        public void OnTriggerEnter2D(Collider2D other)
        {
            onceTriggerEnter2D?.Invoke();
            onceTriggerEnter2D = null;
            onTriggerEnter2D.Invoke();
        }
    }
}
