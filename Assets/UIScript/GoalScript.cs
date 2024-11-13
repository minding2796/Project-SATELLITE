using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UIScript
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class GoalScript : MonoBehaviour
    {
        private Image _parentImage;
        private TextMeshProUGUI _text;
        private Coroutine _coroutine;
        private static GoalScript _instance;

        private void Start()
        {
            _instance = this;
            _text = GetComponent<TextMeshProUGUI>();
            _parentImage = transform.parent.GetComponent<Image>();
        }

        public static void SetGoal(string goal)
        {
            if (_instance) _instance.UpdateGoal("목표 : " + goal);
        }

        private void UpdateGoal(string goal)
        {
            if (goal.Equals("목표 : "))
            {
                _text.text = "";
                _parentImage.color = new Color32(0, 0, 0, 0);
            }
            else
            {
                _parentImage.color = Color.white;
                if (_coroutine != null) StopCoroutine(_coroutine);
                _coroutine = StartCoroutine(TextUpdateFlow(goal));
            }
        }

        private IEnumerator TextUpdateFlow(string text)
        {
            _text.text = "";
            foreach (var c in text)
            {
                _text.text += c;
                yield return null;
            }
        }
    }
}
