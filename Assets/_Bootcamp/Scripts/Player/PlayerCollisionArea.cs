﻿using System;
using _Bootcamp.Scripts.Platform;
using Photon.Pun;
using UnityEngine;

namespace _Bootcamp.Scripts.Player
{
    public class PlayerCollisionArea : MonoBehaviourPunCallbacks
    { 
        private void Start()
        {
            // _playerMovement = GetComponent<PlayerMovement>();
        }
        
        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (hit.transform.CompareTag("Lava"))
            {
                // transform.SetParent(null);
                var pos = hit.transform.GetComponent<DeadSpawner>().GetPosition();
                photonView.RPC("ChangePosRPC", RpcTarget.AllBuffered, pos);
            }
            // else if (hit.transform.CompareTag("Trap"))
                // _playerMovement.SpawnInitPos();
            else if (hit.transform.TryGetComponent<PlatformController>(out var platform))
                platform.PlayerCollided();

            if (hit.transform.TryGetComponent<TrapMovement>(out var trap))
            {
                // transform.SetParent(trap.transform);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.TryGetComponent<PlatformController>(out var platform))
                platform.ShowPlatform();
        }

        [PunRPC]
        private void ChangePosRPC(Vector3 pos)
        {
            transform.localPosition = pos;
        }
    }
}