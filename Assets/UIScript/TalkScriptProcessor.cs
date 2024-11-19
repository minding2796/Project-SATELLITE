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

        private void Awake()
        {
            onScriptFinished.AddListener(EndScript);
            onInterrupt.AddListener(InterruptScript);
        }

        private void Update()
        {
            if (!IsLoaded)
            {
                pfpImage.sprite = PlayerMove.CurrentNpcDetection?.Sr.sprite;
                profileName.text = PlayerMove.CurrentNpcDetection?.npcName;
            }
            else if (!PlayerMove.CurrentNpcDetection) Interrupt();
        }

        public void LoadAndProcess()
        {
            if (IsLoaded) { Next(); return; }
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
