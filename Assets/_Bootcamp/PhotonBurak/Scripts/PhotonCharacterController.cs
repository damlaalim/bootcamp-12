using System;
using _Bootcamp.Scripts.MyExtensions;
using _Bootcamp.Scripts.Player;
using Cinemachine;
using UnityEngine;
using Photon.Pun;
using UnityEditor;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PhotonCharacterController : MonoBehaviourPun
{
    public bool IsMine => photonView.IsMine;
    private bool isNearCube = false;

    [SerializeField] private Transform model;
    private PlayerInput _playerInput;
    private InputAction moveAction;

    [SerializeField] private float speed = 5f;
    public GameObject Mark;
    public GameObject CanvasName;
    public Text Name;


    public bool canMove;
    [SerializeField] private float _sprintMultiplier, _moveSpeed, _gravityValue = -9.81f, _jumpHeight = 6.0f;
    [SerializeField] private Animator _anim;

    private PlayerInputController _playerInputa;
    private CharacterController _characterController;
    private bool _groundedPlayer, _isMove, _isJump, _isIdle, _isRun, _isDance;
    private Vector3 _playerVelocity;
    private Transform _cameraTransform;

    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    private void Start()
    {
        //about ping of server
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 5;
        //about nickname 
        CanvasName.SetActive(true);
        Name.text = GetComponent<PhotonView>().Controller.NickName;
        //about controller and animator
        _playerInputa = GetComponent<PlayerInputController>();
        _characterController = GetComponent<CharacterController>();
        _cameraTransform = Camera.main.transform;
        canMove = _isIdle = true;
        //Cursor.lockState = CursorLockMode.Locked;
        

        if (photonView.IsMine)
        {
            _virtualCamera.enabled = true;
        }
        else
        {
            _virtualCamera.enabled = false;
        }
    }

    private void Update()
    {
        if (photonView.IsMine && canMove)
        {
            Move();
            Actions();
        }
    }

    private void Move()
    {
        _groundedPlayer = _characterController.isGrounded;

        if (_groundedPlayer && _playerVelocity.y < 0)
            _playerVelocity.y = 0f;

        var moveInput = _playerInputa.GetMovement();
        var sprinted = _playerInputa.SprintedThisFrame();

        // Kamera yönüne göre hareket vektörlerini hesapla
        // var moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
        // var cameraForward = _cameraTransform.forward;
        // cameraForward.y = 0;
        // cameraForward.Normalize();
        // var cameraRight = _cameraTransform.right;
        // cameraRight.y = 0;
        // cameraRight.Normalize();

        var move = (_cameraTransform.forward * moveInput.y + _cameraTransform.right * moveInput.x).With(y: 0);
        var sprintMultiplier = sprinted ? _sprintMultiplier : 1;

        _characterController.Move(move * (Time.deltaTime * _moveSpeed * sprintMultiplier));

        if (_playerInputa.JumpedThisFrame() && _groundedPlayer)
        {
            if (!_isJump)
            {
                _anim.CrossFade("Jump", .1f);
                _isJump = true;
                _isRun = _isIdle = _isDance = _isMove = false;
                photonView.RPC("UpdateAnimation", RpcTarget.Others,"Jump",.1f);
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
                photonView.RPC("UpdateAnimation", RpcTarget.Others,"Idle",.1f);
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
                photonView.RPC("UpdateAnimation", RpcTarget.Others,"Run",.1f);
            }
            else if (!sprinted && !_isMove)
            {
                _isMove = true;
                _anim.CrossFade("Walk", .08f);
                _isRun = _isIdle = _isJump = _isDance = false;
                photonView.RPC("UpdateAnimation", RpcTarget.Others,"Walk",.08f);

            }

            _isIdle = false;
        }
        
    }

    private void Actions()
    {
        if (_playerInputa.Danced1ThisFrame())
        {
            _anim.CrossFade("Dance1", .1f);
                    photonView.RPC("UpdateAnimation", RpcTarget.Others,"Dance1",.1f);
        }
        
        else if (_playerInputa.Danced2ThisFrame())
        {
            _anim.CrossFade("Dance2", .1f);
                    photonView.RPC("UpdateAnimation", RpcTarget.Others,"Dance2",.1f);
        }
        
        else
            return;

        _isMove = _isRun = _isIdle = false;
        _isDance = true;
        
        
    }
    [PunRPC]
    void UpdateAnimation(string animationName, float time)
    {
        _anim.CrossFade(animationName, time);
    }
    
    [PunRPC]
    public void SyncRotation(Quaternion rotation)
    {
        model.localRotation = rotation;
    }
}