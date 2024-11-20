using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UIScript
{
    public class TextScriptProcessor : MonoBehaviour
    {
        [SerializeField] private string scriptPath;
        public UnityEvent onScriptFinished;
        public UnityEvent onInterrupt;
        [NonSerialized] private TextMeshProUGUI _text;
        private string[] _scripts;
        private bool[] _importance;
        private int _index;
        private string _currentNotice;
        private IEnumerator _generator;
        public void Start()
        {
            _text = GetComponent<TextMeshProUGUI>();
            LoadScript(scriptPath);
        }

        public void Skip()
        {
            do Next(); while (_index < _importance.Length && !_importance[_index]);
        }

        public void Interrupt()
        {
            _index = 0;
            _scripts = null;
            UpdateScript("");
            onInterrupt.Invoke();
        }
        
        public void ResetScript()
        {
            UpdateScript(_scripts[_index = 0]);
        }

        public void Next()
        {
            if (++_index < _scripts.Length) UpdateScript(_scripts[_index]);
            else onScriptFinished.Invoke();
        }

        public void Previous()
        {
            if (--_index >= 0) _text.text = _scripts[_index];
        }

        public void LoadScript(string path)
        {
            if (string.IsNullOrEmpty(path)) return;
            _scripts = Resources.Load<TextAsset>(path).text.Split("∮").Select(s => s.Trim()).ToArray();
            _importance = new bool[_scripts.Length];
            for (var i = 0; i < _scripts.Length; i++)
            {
                if (!_scripts[i].StartsWith("!important")) continue;
                _importance[i] = true;
                _scripts[i] = _scripts[i]["!important".Length..];
            }
            ResetScript();
        }
        
        public void UpdateScript(string notice)
        {
            if (notice.Equals(_currentNotice)) return;
            _currentNotice = notice.TrimStart().Replace("\n\n", "\n");
            if (_generator?.Current != null) StopCoroutine(_generator);
            StartCoroutine(_generator = ScriptUpdateFlow());
        }

        private IEnumerator ScriptUpdateFlow()
        {
            var text = "";
            foreach (var c in _currentNotice)
            {
                _text.text = text += c;
                yield return null;
            }
            _text.text = _currentNotice;
            LayoutRebuilder.ForceRebuildLayoutImmediate(_text.rectTransform);
        }
    }
}
