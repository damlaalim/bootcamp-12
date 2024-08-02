using Photon.Pun;
using UnityEngine;

namespace _Bootcamp.PhotonBurak.Scripts
{
    public class CanvasTrigger: MonoBehaviour
    {
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
            if (Input.GetKeyDown(KeyCode.K))
            {
                if (Canvas != null)
                {
                    Canvas.enabled = !Canvas.enabled;
                }
            }
        }
    }
}