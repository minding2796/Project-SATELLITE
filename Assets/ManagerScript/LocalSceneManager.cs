using System;
using System.Collections;
using UIScript;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ManagerScript
{
    public class LocalSceneManager : MonoBehaviour
    {
        private RawImage _fadeObject;
        [SerializeField] private Button.ButtonClickedEvent onFadeEnded;
        private void Start()
        {
            _fadeObject = GameObject.FindGameObjectWithTag("FadeObject").GetComponent<RawImage>();
            StartCoroutine(Fade(false, () =>
            {
                _fadeObject.gameObject.SetActive(false);
                onFadeEnded.Invoke();
            }));
        }

        public void LoadScene(string sceneName)
        {
            StartCoroutine(Fade(true, () => SceneManager.LoadScene(sceneName)));
        }

        private IEnumerator Fade(bool isFadeIn, Action callback)
        {
            ScreenTouchExecutor.DisableTouch = true;
            _fadeObject.gameObject.SetActive(true);
            var currentAlpha = isFadeIn ? 0f : 1f;
            while (isFadeIn ? currentAlpha < 1f : currentAlpha > 0f)
            {
                currentAlpha = Mathf.Clamp(currentAlpha + Time.deltaTime * (isFadeIn ? 1 : -1), 0, 1);
                _fadeObject.color = new Color(_fadeObject.color.r, _fadeObject.color.g, _fadeObject.color.b, currentAlpha);
                yield return null;
            }
            ScreenTouchExecutor.DisableTouch = false;
            callback?.Invoke();
        }
    }
}
