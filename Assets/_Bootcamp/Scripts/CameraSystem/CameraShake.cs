using System;
using Cinemachine;
using UnityEngine;

namespace _Bootcamp.Scripts.CameraSystem
{
    public class CameraShake : MonoBehaviour
    {
        public bool test;
        
        [SerializeField] private float _intensity, _time;
        private CinemachineVirtualCamera _cinemachineVirtualCamera;
        private float _shakerTime;

        private void Awake()
        {
            _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        }

        public void ShakeCamera()
        {
            test = false;
            
            var cinemachineBasicMultiChannelPerlin = 
                _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = _intensity;
            _shakerTime = _time;
        }

        private void Update()
        {
            if (test)
                ShakeCamera();
            if (_shakerTime > 0)
            {
                _shakerTime -= Time.deltaTime;
                if (_shakerTime <= 0f)
                {
                    var cinemachineBasicMultiChannelPerlin =
                        _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                    cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
                }   
            }
        }
    }
}