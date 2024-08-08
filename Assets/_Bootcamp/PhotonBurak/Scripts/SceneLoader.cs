using System;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Bootcamp.PhotonBurak.Scripts
{
    public class SceneLoader : MonoBehaviourPunCallbacks
    {
        public bool load;
        public List<string> levelList; 
        
        public void ChangeScene()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel("Office");
            }
        }

        private void Update()
        {
            if (!load || !Input.GetKeyDown(KeyCode.P) || !PhotonNetwork.IsMasterClient)
                return;
            var current = levelList.FindIndex(x => 
                x == SceneManager.GetActiveScene().name) + 1;
            if (current >= levelList.Count)
                current = 0;
            
            PhotonNetwork.LoadLevel(levelList[current]);
        }
    }
}