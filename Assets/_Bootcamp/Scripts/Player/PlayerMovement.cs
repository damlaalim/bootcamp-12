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
        private bool _groundedPlayer, _isMove, _isJump, _isIdle, _isRun;
        private Vector3 _playerVelocity;
        private Transform _cameraTransform;

        private void Start()
        {
            _playerInput = GetComponent<PlayerInputController>();
            _characterController = GetComponent<CharacterController>();

            _cameraTransform = Camera.main.transform;
            canMove = _isIdle = true;
        }

        private void Update()
        {
            Move();

            if (Input.GetKeyDown(KeyCode.F1))
            {
                _anim.CrossFade("dance", .2f);
                _isMove = _isRun = _isIdle =  false;
            }
        }

        private void Move()
        {
            _groundedPlayer = _characterController.isGrounded;
            if (_groundedPlayer && _isJump)
                _isJump = false;
            
            if (_groundedPlayer && _playerVelocity.y < 0) _playerVelocity.y = 0f;

            var moveInput = _playerInput.GetMovement();
            var sprinted = _playerInput.SprintedThisFrame();
            var move = (_cameraTransform.forward * moveInput.y + _cameraTransform.right * moveInput.x).With(y: 0);
            var sprintMultiplier = sprinted ? _sprintMultiplier : 1;
            
            _characterController.Move(move * (Time.deltaTime * _moveSpeed * sprintMultiplier));

            if (_playerInput.JumpedThisFrame() && _groundedPlayer)
            {
                if (!_isJump)
                {
                    _isJump = true;
                    _isRun = _isIdle = _isMove = false;
                    _anim.CrossFade("Jump", 0f);
                }
                _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * _gravityValue);
            }

            _playerVelocity.y += _gravityValue * Time.deltaTime;
            _characterController.Move(_playerVelocity * Time.deltaTime);
            
            if (moveInput == Vector2.zero)
            {
                if (!_isIdle && (_isMove || _isRun) && !_isJump)
                {
                    _anim.CrossFade("Idle", .2f);
                    _isIdle = true;
                }
                _isMove = _isRun = false;
                return;
            }

            if (moveInput != Vector2.zero && !_isJump)
            {
                _anim.SetFloat("WalkSpeed", moveInput.y < 0 ? -1 : 1);

                if (sprinted && !_isRun)
                {
                    _isRun = true;
                    _anim.CrossFade("Run", .1f);
                    _isMove = false;
                }
                else if (!sprinted && !_isMove)
                {
                    _isMove = true;
                    _anim.CrossFade("Walk", .1f);
                    _isRun = false;
                }
                
                _isIdle = false;
            }
        }
    }
}
