using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UIScript
{
    public class TextScriptProcessor : MonoBehaviour
    {
        [SerializeField] private string scriptPath;
        [SerializeField] private Button.ButtonClickedEvent onScriptFinished;
        private TextMeshProUGUI _text;
        private string[] _scripts;
        private bool[] _importance;
        private int _index;
        private void Start()
        {
            _text = GetComponent<TextMeshProUGUI>();
            LoadScript(scriptPath);
        }

        public void Skip()
        {
            do Next(); while (_index < _importance.Length && !_importance[_index]);
        }
        
        public void ResetScript()
        {
            _text.text = _scripts[_index = 0];
        }

        public void Next()
        {
            if (++_index < _scripts.Length) _text.text = _scripts[_index];
            else onScriptFinished.Invoke();
        }

        public void Previous()
        {
            if (--_index >= 0) _text.text = _scripts[_index];
        }

        public void LoadScript(string path)
        {
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
    }
}
