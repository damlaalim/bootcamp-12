using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

namespace _Bootcamp.PhotonBurak.Scripts
{
    public class SceneLoader : MonoBehaviourPunCallbacks
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                ChangeScene();
            }

            if (Input.GetKeyDown((KeyCode.L)))
            {
                ChangeScene2();
            }
        }

        private void ChangeScene2()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel("Puzzle"); 
            }
        }

        void ChangeScene()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel("OfficePhoton");
            }
        }
    }
}
