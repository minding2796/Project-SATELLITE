using System;
using System.Collections.Generic;
using UIScript;
using UnityEngine;

namespace PlayerScript
{
    public class PlayerNpcDetection : MonoBehaviour
    {
        public List<string> scriptPaths;
        public string npcName;
        [NonSerialized] public SpriteRenderer Sr;
        private int _index;

        private void Start()
        {
            Sr = GetComponent<SpriteRenderer>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            PlayerMove.CurrentNpcDetection = this;
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            if (PlayerMove.CurrentNpcDetection == this) PlayerMove.CurrentNpcDetection = null;
        }

        public string GetCurrentScriptPath()
        {
            return scriptPaths[Mathf.Clamp(_index, 0, scriptPaths.Count - 1)];
        }

        public void NextScript()
        {
            _index++;
        }
    }
}
