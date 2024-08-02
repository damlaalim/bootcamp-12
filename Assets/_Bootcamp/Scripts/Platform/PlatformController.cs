﻿using System;
using System.Collections;
using _Bootcamp.Scripts.MyExtensions;
using DG.Tweening;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace _Bootcamp.Scripts.Platform
{
    public class PlatformController : MonoBehaviourPunCallbacks
    {
        [SerializeField] private bool _platformIsTimeLimited, _visible;
        [SerializeField] private float _countdownDelay, _initPosY, _fallDelay, _showDelay, _returnDelay = 5;

        private bool _isShow, _countdownIsStarted;
        private MeshRenderer _mesh;

        private void Awake()
        {
            _mesh = GetComponent<MeshRenderer>();
            
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

        [PunRPC]
        private void FallPlatform()
        {
            var initY = transform.position.y;
            transform.DOMoveY(0, _fallDelay).OnComplete(() =>
            {
                transform.DOMoveY(initY, _returnDelay);
            });
        }

        public void PlayerCollided()
        {
            if (_platformIsTimeLimited && !_countdownIsStarted)
            {
                StartCoroutine(Countdown_Routine());
            }
        }

        private IEnumerator Countdown_Routine()
        {
            _countdownIsStarted = true;

            yield return new WaitForSeconds(_countdownDelay);
            
            photonView.RPC("FallPlatform", RpcTarget.AllBuffered);
        }
    }
}