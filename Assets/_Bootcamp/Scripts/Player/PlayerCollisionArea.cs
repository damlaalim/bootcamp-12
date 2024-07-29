using System;
using _Bootcamp.Scripts.Platform;
using UnityEngine;

namespace _Bootcamp.Scripts.Player
{
    public class PlayerCollisionArea : MonoBehaviour
    {
        private PlayerMovement _playerMovement;
        
        private void Start()
        {
            _playerMovement = GetComponent<PlayerMovement>();
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            // Level başına spawnla
            if (hit.transform.CompareTag("Lava"))
                _playerMovement.SpawnInitPos();
            else if (hit.transform.CompareTag("Trap"))
                _playerMovement.SpawnInitPos();
            else if (hit.transform.TryGetComponent<PlatformController>(out var platform))
                platform.PlayerCollided();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.TryGetComponent<PlatformController>(out var platform))
                platform.ShowPlatform();
        }
    }
}