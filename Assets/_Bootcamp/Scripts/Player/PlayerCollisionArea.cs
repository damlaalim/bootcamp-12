using System;
using _Bootcamp.Scripts.Platform;
using UnityEngine;

namespace _Bootcamp.Scripts.Player
{
    public class PlayerCollisionArea : MonoBehaviour
    {
        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            // Level başına spawnla
            if (hit.transform.CompareTag("Lava"))
                ;
            else if (hit.transform.CompareTag("Trap"))
                ;
            else if (hit.transform.TryGetComponent<PlatformController>(out var platform))
                platform.PlayerCollided();
        }
    }
}