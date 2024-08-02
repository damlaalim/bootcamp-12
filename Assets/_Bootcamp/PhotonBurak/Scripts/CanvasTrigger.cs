using Photon.Pun;
using UnityEngine;

namespace _Bootcamp.PhotonBurak.Scripts
{
    public class CanvasTrigger : MonoBehaviourPun
    {
        [SerializeField] private PhotonCharacterController _character;
        public Canvas Canvas;

        private void Start()
        {
            if (photonView.IsMine && Canvas != null)
            {
                Canvas.enabled = false;
            }
        }

        private void Update()
        {
            if (photonView.IsMine && Input.GetKeyDown(KeyCode.K) && Canvas != null)
            {
                var enabled = !Canvas.enabled;
                Canvas.enabled = enabled;
                _character.canMove = !enabled;
                _character.openCanvas = enabled; 
                Cursor.lockState = !enabled ? CursorLockMode.Locked : CursorLockMode.None;

                // Canvas durumunu diğer oyunculara bildir
                photonView.RPC("SyncCanvasState", RpcTarget.Others, enabled);
            }
        }

        [PunRPC]
        private void SyncCanvasState(bool enabled)
        {
            if (!photonView.IsMine && Canvas != null)
            {
                Canvas.enabled = enabled;
                _character.canMove = !enabled;
                _character.openCanvas = enabled;
                Cursor.lockState = !enabled ? CursorLockMode.Locked : CursorLockMode.None;
            }
        }
    }
}