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
        private int _index;
        private void Start()
        {
            _text = GetComponent<TextMeshProUGUI>();
            _scripts = Resources.Load<TextAsset>(scriptPath).text.Split("∮").Select(s => s.Trim()).ToArray();
            _text.text = _scripts[_index = 0];
        }

        public void Next()
        {
            if (++_index < _scripts.Length) _text.text = _scripts[_index];
            else onScriptFinished.Invoke();
        }
    }
}
