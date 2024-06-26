using UnityEngine;

namespace _Bootcamp.Scripts.Player
{
    public class PlayerInputController : MonoBehaviour
    {
        private PlayerControls _playerControls;
        
        private void Awake()
        {
            _playerControls = new PlayerControls();
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnEnable()
        {
            _playerControls.Enable();
        }

        private void OnDisable()
        {
            _playerControls.Disable();
        }

        public Vector2 GetMovement()
        {
            return _playerControls.Player.Move.ReadValue<Vector2>();
        }

        public Vector2 GetLookDelta()
        {
            return _playerControls.Player.Look.ReadValue<Vector2>();
        }

        public bool JumpedThisFrame()
        {
            return _playerControls.Player.Jump.triggered;
        }
        
        public bool SprintedThisFrame()
        {
            return _playerControls.Player.Sprint.IsInProgress();
        }
        
        public bool InteractedThisFrame()
        {
            return _playerControls.Player.Interaction.triggered;
        }
    }
}