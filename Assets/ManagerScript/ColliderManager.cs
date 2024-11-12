using UnityEngine;
using UnityEngine.UI;

namespace ManagerScript
{
    public class ColliderManager : MonoBehaviour
    {
        public Button.ButtonClickedEvent onTriggerEnter2D;

        public void OnTriggerEnter2D(Collider2D other)
        {
            onTriggerEnter2D.Invoke();
        }
    }
}
