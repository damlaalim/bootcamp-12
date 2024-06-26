using _Bootcamp.Scripts.CanvasSystem;
using _Bootcamp.Scripts.Interactable;
using UnityEngine;
using Zenject;

namespace _Bootcamp.Scripts.Player
{
    public class PlayerInteractionController : MonoBehaviour
    {
        [SerializeField] private float _maxDistance = 50;
        private Camera _mainCam;
        private PlayerInputController _playerInputController;

        [Inject] private InGameCanvas _inGameCanvas;

        private void Start()
        {
            _playerInputController = GetComponent<PlayerInputController>();
            _mainCam = Camera.main;
        }
        
        private void Update()
        {
            InteractionTrigger();
            
            var playerInteractionThisFrame = _playerInputController.InteractedThisFrame();
            if (playerInteractionThisFrame)
                Interacted();
        }
        
        private void InteractionTrigger()
        {
            var ray = _mainCam.ScreenPointToRay(Input.mousePosition);
            var interactable = Physics.Raycast(ray, out var hit, _maxDistance) && hit.transform.TryGetComponent<InteractableController>(out _);

            _inGameCanvas.ShowInteractionText(interactable);
        }
        
        private void Interacted()
        {
            var ray = _mainCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, _maxDistance) && hit.transform.TryGetComponent<InteractableController>(out var interactable))
                interactable.Do();
        }
    }
}