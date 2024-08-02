using System;
using System.Collections;
using _Bootcamp.Scripts.MyExtensions;
using DG.Tweening;
using UnityEngine;

namespace _Bootcamp.Scripts.Platform
{
    public class TrapController : MonoBehaviour
    {
        [SerializeField] private float _targetXPos, _delay, _speed;

        private bool _move;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !_move)
            {
                StartCoroutine(Move());
            }
        }

        private IEnumerator Move()
        {
            _move = true;
            yield return new WaitForSeconds(_delay);
            var targetPos = transform.localPosition.With(x: _targetXPos);
            transform.DOLocalMove(targetPos, _speed).SetLoops(2, LoopType.Yoyo).OnComplete(() => _move = false);
        }
    }
}