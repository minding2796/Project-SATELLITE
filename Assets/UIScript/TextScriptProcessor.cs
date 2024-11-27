using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UIScript
{
    [RequireComponent(typeof(TextMeshProUGUI))]
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
        private Coroutine _coroutine;
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
            _scripts = (Resources.Load<TextAsset>(path)?.text ?? "").Split("∮").Select(s => s.Trim()).ToArray();
            _importance = new bool[_scripts.Length];
            for (var i = 0; i < _scripts.Length; i++)
            {
                if (!_scripts[i].StartsWith("!important")) continue;
                _importance[i] = true;
                _scripts[i] = _scripts[i]["!important".Length..];
            }
            ResetScript();
        }

        public void UpdateScript(string script)
        {
            if (script.Equals(_currentNotice)) return;
            _currentNotice = script;
            if (_coroutine != null) StopCoroutine(_coroutine);
            _coroutine = StartCoroutine(ScriptUpdateFlow());
        }

        private IEnumerator ScriptUpdateFlow()
        {
            var text = "";
            _text.text = text;
            LayoutRebuilder.ForceRebuildLayoutImmediate(_text.rectTransform);
            foreach (var c in _currentNotice)
            {
                _text.text = text += c;
                yield return new WaitForFixedUpdate();
            }
            _text.text = _currentNotice;
            LayoutRebuilder.ForceRebuildLayoutImmediate(_text.rectTransform);
        }
    }
}
