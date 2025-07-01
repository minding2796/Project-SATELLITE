using System;
using Unity.Cinemachine;
using UnityEngine;

namespace PlayerScript
{
    [RequireComponent(typeof(CinemachineCamera))]
    public class CinemachineRadius : MonoBehaviour
    {
        private CinemachineCamera _camera;
        private void Start()
        {
            _camera = GetComponent<CinemachineCamera>();
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player")) _camera.Priority = 1;
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player")) _camera.Priority = -1;
        }
    }
}
