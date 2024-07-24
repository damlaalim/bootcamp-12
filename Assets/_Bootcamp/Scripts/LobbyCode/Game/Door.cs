using System;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class Door : NetworkBehaviour
    {
        [SerializeField] private List<Switch> _switches;
        [SerializeField] private NetworkAnimator _animControl;

        private Dictionary<Switch, bool> _activeSwitches = new Dictionary<Switch, bool>();

        

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            if (IsServer)
            {
                foreach (Switch doorSwitch in _switches)
                {
                    if (doorSwitch == null)
                    {
                        Debug.LogError("One of the switches is not assigned");
                        continue;
                    }

                    doorSwitch.OnSwitchChanged += OnSwitchChanged;
                    _activeSwitches.Add(doorSwitch, false);
                }
            }
        }

        private void OnSwitchChanged(Switch doorSwitch, bool isActive)
        {
            if (doorSwitch == null) return;

            _activeSwitches[doorSwitch] = isActive;

            foreach (var switchStatus in _activeSwitches.Values)
            {
                if (!switchStatus)
                {
                    return;
                }
            }
            Debug.Log("Open the door");
            _animControl.SetTrigger("OpenDoor");
         
        }
    }

}