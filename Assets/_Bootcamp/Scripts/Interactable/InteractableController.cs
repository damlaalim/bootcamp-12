using UnityEngine;

namespace _Bootcamp.Scripts.Interactable
{
    public class InteractableController : MonoBehaviour, IInteractable
    {
        [SerializeField] private Canvas _canvas;

        public Canvas ICanvas
        {
            get => _canvas;
            set { }
        }

        public void Do()
        {
            Debug.Log($"Interaction, obj: {gameObject.name}", gameObject);
        }

        public void ShowCanvas(bool show)
        {
            ICanvas.enabled = show;
        }
    }
}