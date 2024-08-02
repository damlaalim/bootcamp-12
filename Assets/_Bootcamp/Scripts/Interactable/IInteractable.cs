using UnityEngine;

namespace _Bootcamp.Scripts.Interactable
{
    public interface IInteractable
    {
        public Canvas ICanvas { get; set; }
        public void Do();

        public void ShowCanvas(bool show);
        public void ChangePos(Camera mainCam);
    }
}