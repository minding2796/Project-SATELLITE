using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UIScript
{
    public class TextScriptProcessor : MonoBehaviour
    {
        [SerializeField] private string scriptPath;
        public Button.ButtonClickedEvent onScriptFinished;
        public Button.ButtonClickedEvent onInterrupt;
        [NonSerialized] protected TextMeshProUGUI Text;
        private string[] _scripts;
        private bool[] _importance;
        private int _index;
        private string _currentNotice;
        private IEnumerator _generator;
        public void Start()
        {
            Text = GetComponent<TextMeshProUGUI>();
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
            if (--_index >= 0) Text.text = _scripts[_index];
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
            var text = Text.text.ToCharArray().Concat(new char[Mathf.Max(_currentNotice.Length - Text.text.Length, 0)]).ToArray();
            for (var i = 0; i < _currentNotice.Length; i++)
            {
                if (text[i] == _currentNotice[i]) continue;
                text[i] = _currentNotice[i];
                Text.text = string.Join("", text).Trim();
                yield return null;
            }
            for (var i = _currentNotice.Length; i < text.Length; i++)
            {
                text[i] = ' ';
                Text.text = string.Join("", text).Trim();
                yield return null;
            }
            Text.text = _currentNotice;
        }
    }
}
