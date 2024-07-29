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
        
        [SerializeField] private bool _platformIsTimeLimited, _visible;
        [SerializeField] private float _countdownDelay, _initPosY, _fallDelay, _showDelay;

        private bool _isShow, _countdownIsStarted;
        private MeshRenderer _mesh;
        private Collider _collider;

        private void Awake()
        {
            _mesh = GetComponent<MeshRenderer>();
            _collider = GetComponent<Collider>();
            
            _mesh.enabled = _isShow = _visible;
        }

        public void ShowPlatform()
        {
            if (_isShow) return;

            _isShow = true;
            
            _mesh.enabled = true;
            var targetPos = transform.position;
            transform.position = targetPos.With(y: _initPosY);
            transform.DOMoveY(targetPos.y, _showDelay);
        }

        private void FallPlatform()
        {
            transform.DOMoveY(0, _fallDelay).OnComplete(() => _isShow = false);
        }

        public void PlayerCollided()
        {
            if (!isTrap)
            {
                // _platformManager.PlatformTriggered(this);
                
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