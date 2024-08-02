using _Bootcamp.Scripts.Interactable;
using Photon.Pun;
using UnityEngine;

namespace _Bootcamp.PhotonBurak.Scripts
{
    public class SceneLoaderInteractible:MonoBehaviourPunCallbacks, IInteractable
    {
        [SerializeField] private string SceneName;
        [SerializeField]AnalyticsManager analyticsManager;
        public Canvas ICanvas { get; set; }
        [SerializeField] private Canvas _canvas;
        public void Do()
        {
            analyticsManager.SendCustomEvent();
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel(SceneName);
            }
        }

        public void ShowCanvas(bool show)
        {
            _canvas.enabled = show;
        }

        public void ChangePos(Camera mainCam)
        {
            var mainCamRot = mainCam.transform.rotation;
            _canvas.transform.LookAt(_canvas.transform.position + mainCamRot * Vector3.forward,
                mainCamRot * Vector3.up);
        }
    }
}