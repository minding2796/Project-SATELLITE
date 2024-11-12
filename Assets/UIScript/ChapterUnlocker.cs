using ManagerScript;
using UnityEngine;

namespace UIScript
{
    public class ChapterUnlocker : MonoBehaviour
    {
        [SerializeField] private int requiredChapter;
        private void Start()
        {
            if (LocalDataManager.MaxClearedChapter >= requiredChapter) gameObject.SetActive(false);
        }
    }
}
