using System;
using PlayerScript;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UIScript
{
    public class TalkScriptProcessor : TextScriptProcessor
    {
        private static bool IsLoaded { get; set; }
        private Action _nextScriptAction;
        [SerializeField] private Image pfpImage;
        [SerializeField] private TextMeshProUGUI profileName;
        private Image _profileNameBackground;

        private void Awake()
        {
            onScriptFinished.AddListener(EndScript);
            onInterrupt.AddListener(InterruptScript);
            _profileNameBackground = profileName.transform.parent.GetComponent<Image>();
        }

        private void Update()
        {
            if (IsLoaded && !PlayerMove.CurrentNpcDetection) Interrupt();
            if (Input.GetKeyDown(KeyCode.E)) LoadAndProcess();
            if (IsLoaded) return;
            pfpImage.sprite = PlayerMove.CurrentNpcDetection?.Sr.sprite;
            profileName.text = PlayerMove.CurrentNpcDetection?.npcName;
            _profileNameBackground.color = new Color(1, 1, 1, string.IsNullOrEmpty(profileName.text) ? 0 : 1);
            if (PlayerMove.CurrentNpcDetection && PlayerMove.CurrentNpcDetection.autoProgress) LoadAndProcess();
        }

        public void LoadAndProcess()
        {
            if (IsLoaded)
            {
                if (PlayerMove.CurrentNpcDetection.preventProgress) return;
                Next();
                return;
            }
            if (!PlayerMove.CurrentNpcDetection) return;
            LoadScript(PlayerMove.CurrentNpcDetection.GetCurrentScriptPath());
            _nextScriptAction = PlayerMove.CurrentNpcDetection.NextScript;
            IsLoaded = true;
        }

        private void EndScript()
        {
            UpdateScript("");
            IsLoaded = false;
            _nextScriptAction();
        }

        private static void InterruptScript()
        {
            IsLoaded = false;
        }
    }
}
