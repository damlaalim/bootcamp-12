using _Bootcamp.Scripts.MyExtensions;
using UnityEngine;
using UnityEngine.Rendering;


namespace _Bootcamp.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public bool canMove;
        [SerializeField] private float _sprintMultiplier, _moveSpeed, _gravityValue = -9.81f, _jumpHeight = 6.0f;
        [SerializeField] private Animator _anim;
        
        private PlayerInputController _playerInput;
        private CharacterController _characterController;
        private bool _groundedPlayer, _isMove, _isJump, _isIdle, _isRun, _isDance;
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
            Actions();
            Move();
        }

        private void Move()
        {
            _groundedPlayer = _characterController.isGrounded;

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
                    _anim.CrossFade("Jump", .1f);
                    _isJump = true;
                    _isRun = _isIdle = _isDance = _isMove = false;
                }
                _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * _gravityValue);
            }

            _playerVelocity.y += _gravityValue * Time.deltaTime;
            _characterController.Move(_playerVelocity * Time.deltaTime);
            
            _groundedPlayer = _characterController.isGrounded;

            if (moveInput == Vector2.zero)
            {
                if (!_isIdle && _groundedPlayer && !_isDance)
                {
                    _anim.CrossFade("Idle", .1f);
                    _isIdle = true;
                    _isMove = _isRun = _isJump = false;
                }
                return;
            }

            if (moveInput != Vector2.zero && _groundedPlayer)
            {
                _anim.SetFloat("WalkSpeed", moveInput.y < 0 ? -1 : 1);

                if (sprinted && !_isRun)
                {
                    _isRun = true;
                    _anim.CrossFade("Run", .1f);
                    _isMove = _isIdle = _isJump = _isDance = false;
                }
                else if (!sprinted && !_isMove)
                {
                    _isMove = true;
                    _anim.CrossFade("Walk", .08f);
                    _isRun = _isIdle = _isJump = _isDance = false;
                }
                
                _isIdle = false;
            }
        }

        private void Actions()
        {
            if (_playerInput.Danced1ThisFrame())
                _anim.CrossFade("Dance1", .1f);
            else if (_playerInput.Danced2ThisFrame())
                _anim.CrossFade("Dance2", .1f);
            else 
                return;
            
            _isMove = _isRun = _isIdle =  false;
            _isDance = true;
        }
    }
}
