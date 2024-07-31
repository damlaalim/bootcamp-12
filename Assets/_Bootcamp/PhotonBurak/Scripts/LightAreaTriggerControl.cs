using System;
using UnityEngine;

namespace _Bootcamp.PhotonBurak.Scripts
{
    public class LightAreaTriggerControl: MonoBehaviour
    {
        [SerializeField] private LightSequenceManager _lightSequenceManager;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(_lightSequenceManager.LightRoutine());
            }
        }
    }
}