using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

namespace _Bootcamp.PhotonBurak.Scripts
{
    public class SceneLoader : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                ChangeScene();
            }
        }

        void ChangeScene()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel("Gameplay2"); // Geçmek istediğiniz sahnenin adını buraya yazın
            }
        }
    }
}
