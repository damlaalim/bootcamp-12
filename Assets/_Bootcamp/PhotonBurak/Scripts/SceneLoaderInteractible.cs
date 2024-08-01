using _Bootcamp.Scripts.Interactable;
using Photon.Pun;
using UnityEngine;

namespace _Bootcamp.PhotonBurak.Scripts
{
    public class SceneLoaderInteractible:MonoBehaviourPunCallbacks, IInteractable
    {
        [SerializeField] private string SceneName;
        public Canvas ICanvas { get; set; }
        [SerializeField] private Canvas _canvas;
        public void Do()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel(SceneName);
            }
        }

        public void ShowCanvas(bool show)
        {
            _canvas.enabled = show;
        }
    }
}