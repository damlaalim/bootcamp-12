using System;
using System.Collections;
using _Bootcamp.Scripts.MyExtensions;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace _Bootcamp.Scripts.Platform
{
    public class PlatformController : MonoBehaviour
    {
        public bool isTrap;
        [SerializeField] private bool _platformIsTimeLimited;
        [SerializeField] private float _countdownDelay;
        [SerializeField] private MeshRenderer _mesh;
        [SerializeField] private Collider _collider;
        [SerializeField] private float _initPosY;
        
        [Inject] private PlatformManager _platformManager;
        private bool _isShow, _countdownIsStarted;

        private void Awake()
        {
            _mesh.enabled = _collider.enabled = false;
        }

        public void ShowPlatform()
        {
            if (_isShow) return;

            _isShow = true;
            
            _mesh.enabled = _collider.enabled = true;
            var targetPos = transform.position;
            transform.position = targetPos.With(y: _initPosY);
            transform.DOMoveY(targetPos.y, .2f);
        }

        private void FallPlatform()
        {
            _isShow = false;
            // animasyon
        }

        public void PlayerCollided()
        {
            if (!isTrap)
            {
                _platformManager.PlatformTriggered(this);
                
                if (_platformIsTimeLimited && !_countdownIsStarted)
                    StartCoroutine(Countdown_Routine());
            }
            else
                ;
        }

        private IEnumerator Countdown_Routine()
        {
            _countdownIsStarted = true;

            yield return new WaitForSeconds(_countdownDelay);
            
            FallPlatform();
        }
    }
}