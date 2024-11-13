using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UtilScripts;

namespace UIScript
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class NoticeScript : MonoBehaviour
    {
        private Image _parentImage;
        private TextMeshProUGUI _text;
        private Coroutine _coroutine;
        private string _currentNotice;
        private static NoticeScript _instance;

        private void Start()
        {
            _instance = this;
            _text = GetComponent<TextMeshProUGUI>();
            _parentImage = transform.parent.GetComponent<Image>();
        }

        public static void RemoveNotice(string notice)
        {
            if (_instance) _instance.UpdateNotice(_instance._currentNotice.ReplaceFirst(notice, string.Empty));
        }

        public static void ReplaceNotice(string oldNotice, string newNotice)
        {
            if (_instance) _instance.UpdateNotice(_instance._currentNotice.ReplaceFirst(oldNotice, newNotice));
        }

        public static void AddNotice(string notice)
        {
            if (_instance) _instance.UpdateNotice(_instance._currentNotice + "\n" + notice);
        }
        
        public static void SetNotice(string notice)
        {
            if (_instance) _instance.UpdateNotice(notice);
        }

        private void UpdateNotice(string notice)
        {
            _currentNotice = notice.Replace("\n\n", "\n");
            _parentImage.color = string.IsNullOrEmpty(_currentNotice) ? new Color32(0, 0, 0, 0) : Color.white;
            if (_coroutine != null) StopCoroutine(_coroutine);
            _coroutine = StartCoroutine(TextUpdateFlow());
        }

        private IEnumerator TextUpdateFlow()
        {
            _text.text = "";
            foreach (var c in _currentNotice)
            {
                _text.text += c;
                yield return null;
            }
        }
    }
}
