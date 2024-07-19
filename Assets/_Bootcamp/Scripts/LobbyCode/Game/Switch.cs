using System;
using Unity.Netcode;
using UnityEngine;

namespace Game
{
    public class Switch : NetworkBehaviour
    {
        private NetworkVariable<bool> _isActive = new NetworkVariable<bool>();

        public delegate void SwitchChanged(Switch doorSwitch, bool isActive);
        public event SwitchChanged OnSwitchChanged;
        
        
        public override void OnNetworkSpawn()
        {
             base.OnNetworkSpawn();
             _isActive.OnValueChanged += OnValueChanged;
        }

        private void OnValueChanged(bool wasActive, bool isActive)
        {
            if (isActive)
            {
                 Debug.Log("ısactive");
            }
            else
            {
                Debug.Log("is not active");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            
            OnSwitchChangedServerRpc(true);
        }

        private void OnTriggerExit(Collider other)
        {
            OnSwitchChangedServerRpc(false);
        }

        [ServerRpc(RequireOwnership = false)]
        private void OnSwitchChangedServerRpc(bool isActive)
        {
            _isActive.Value = isActive;
            OnSwitchChanged?.Invoke(this,isActive);
        }
        
    }
}