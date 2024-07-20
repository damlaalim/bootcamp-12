using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace _Bootcamp.Scripts.Platform
{
    public class PlatformController : MonoBehaviour
    {
        public bool isTrap;
        [SerializeField] private bool _platformIsTimeLimited;
        [SerializeField] private float _countdownDelay;

        [Inject] private PlatformManager _platformManager;
        private bool _isShow, _countdownIsStarted;

        public void ShowPlatform()
        {
            if (_isShow) return;

            _isShow = true;
            
            // animasyon
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