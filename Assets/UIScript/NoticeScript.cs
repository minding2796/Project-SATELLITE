using System.Collections;
using System.Linq;
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
            if (notice.Equals(_currentNotice)) return;
            _currentNotice = notice.TrimStart().Replace("\n\n", "\n");
            if (_coroutine != null) StopCoroutine(_coroutine);
            _coroutine = StartCoroutine(TextUpdateFlow());
        }

        private IEnumerator TextUpdateFlow()
        {
            if (!string.IsNullOrEmpty(_currentNotice)) _parentImage.color = Color.white;
            var text = _text.text.ToCharArray().Concat(new char[Mathf.Max(_currentNotice.Length - _text.text.Length, 0)]).ToArray();
            for (var i = 0; i < _currentNotice.Length; i++)
            {
                if (text[i] == _currentNotice[i]) continue;
                text[i] = _currentNotice[i];
                _text.text = string.Join("", text).Trim();
                yield return null;
            }
            for (var i = _currentNotice.Length; i < text.Length; i++)
            {
                text[i] = ' ';
                _text.text = string.Join("", text).Trim();
                yield return null;
            }
            _text.text = _currentNotice;
            if (string.IsNullOrEmpty(_currentNotice)) _parentImage.color = new Color32(0, 0, 0, 0);
        }
    }
}
