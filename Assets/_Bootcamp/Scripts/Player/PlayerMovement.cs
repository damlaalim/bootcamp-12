using _Bootcamp.Scripts.MyExtensions;
using UnityEngine;
using UnityEngine.Rendering;


namespace _Bootcamp.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public bool canMove;
        [SerializeField] private float _sprintMultiplier, _moveSpeed, _gravityValue = -9.81f, _jumpHeight = 1.0f;
        [SerializeField] private Animator _anim;
        
        private PlayerInputController _playerInput;
        private CharacterController _characterController;
        private bool _groundedPlayer, _isMove;
        private Vector3 _playerVelocity;
        private Transform _cameraTransform;

        private void Start()
        {
            _playerInput = GetComponent<PlayerInputController>();
            _characterController = GetComponent<CharacterController>();

            _cameraTransform = Camera.main.transform;
            canMove = true;
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            if (!canMove)
            {
                if (_isMove)
                    _anim.CrossFade("Idle", .1f);
                _isMove = false;
                return;
            }

            if (!_isMove)
            {
                _isMove = true;
                _anim.CrossFade("Walk", .1f);
            }
            
            _groundedPlayer = _characterController.isGrounded;
            if (_groundedPlayer && _playerVelocity.y < 0) _playerVelocity.y = 0f;

            var moveInput = _playerInput.GetMovement();
            var move = (_cameraTransform.forward * moveInput.y + _cameraTransform.right * moveInput.x).With(y: 0);
            var sprintMultiplier = _playerInput.SprintedThisFrame() ? _sprintMultiplier : 1;
            
            _characterController.Move(move * (Time.deltaTime * _moveSpeed * sprintMultiplier));
            
            if (_playerInput.JumpedThisFrame() && _groundedPlayer) 
                _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * _gravityValue);

            _playerVelocity.y += _gravityValue * Time.deltaTime;
            _characterController.Move(_playerVelocity * Time.deltaTime);
        }
    }
}
