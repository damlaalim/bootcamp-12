using _Bootcamp.Scripts.CanvasSystem;
using _Bootcamp.Scripts.Interactable;
using DialogueEditor;
using UnityEngine;
using Zenject;

namespace _Bootcamp.Scripts.Player
{
    public class PlayerInteractionController : MonoBehaviour
    {
        [SerializeField] private float _maxDistance = 50;
        private Camera _mainCam;
        private PlayerInputController _playerInputController;
        private PlayerMovement _playerMovement;

        [Inject] private InGameCanvas _inGameCanvas;

        private void Start()
        {
            _playerInputController = GetComponent<PlayerInputController>();
            _playerMovement = GetComponent<PlayerMovement>();
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
            if (ConversationManager.Instance != null && ConversationManager.Instance.IsConversationActive)
            {
                _inGameCanvas.ShowInteractionText(false);
                Cursor.lockState = CursorLockMode.None;
                _playerMovement.canMove = false;
                return;
            }
            
            Cursor.lockState = CursorLockMode.Locked;
            _playerMovement.canMove = true;

            var ray = _mainCam.ScreenPointToRay(Input.mousePosition);
            var interactable = Physics.Raycast(ray, out var hit, _maxDistance) && hit.transform.TryGetComponent<IInteractable>(out _);

            _inGameCanvas.ShowInteractionText(interactable);
        }
        
        private void Interacted()
        {
            if (ConversationManager.Instance != null && ConversationManager.Instance.IsConversationActive)
            {
                ConversationManager.Instance.PressSelectedOption();
                return;
            }
            
            var ray = _mainCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, _maxDistance) && hit.transform.TryGetComponent<IInteractable>(out var interactable))
                interactable.Do();
        }
    }
}