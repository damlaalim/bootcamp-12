using Photon.Pun;
using UnityEngine;

namespace _Bootcamp.PhotonBurak.Scripts
{
    public class CanvasTrigger : MonoBehaviourPunCallbacks
    {
        [SerializeField] private PhotonCharacterController _character;
        public Canvas Canvas;

        private void Start()
        {
            if (Canvas != null)
            {
                Canvas.enabled = false;
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K) && Canvas != null)
            {
                var enabled = !Canvas.enabled;
                Canvas.enabled = enabled;
                _character.canMove = !enabled;
                _character.openCanvas = enabled; 
                Cursor.lockState = !enabled ? CursorLockMode.Locked : CursorLockMode.None;
            }
        }
    }
}