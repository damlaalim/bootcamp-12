using _Bootcamp.Scripts.MyExtensions;
using _Bootcamp.Scripts.Player;
using UnityEngine;
using Photon.Pun;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class PhotonCharacterController : MonoBehaviourPunCallbacks
{
   private PlayerInput _playerInput;

   private InputAction moveAction;

   [SerializeField] private float speed = 5f;

   public GameObject Mark;

   public GameObject CanvasName;
   public Text Name;
   
   private bool isNearCube = false;

   public bool canMove;
   [SerializeField] private float _sprintMultiplier, _moveSpeed, _gravityValue = -9.81f, _jumpHeight = 6.0f;
   [SerializeField] private Animator _anim;
        
   private PlayerInputController _playerInputa;
   private CharacterController _characterController;
   private bool _groundedPlayer, _isMove, _isJump, _isIdle, _isRun, _isDance;
   private Vector3 _playerVelocity;
   private Transform _cameraTransform;
   
   private void Start()
   {
       PhotonNetwork.SendRate = 60;
       PhotonNetwork.SerializationRate = 5;
       
      /*_playerInput = GetComponent<PlayerInput>();
      moveAction = _playerInput.actions.FindAction("Move");
      */
      
       CanvasName.SetActive(true);
       Name.text = GetComponent<PhotonView>().Controller.NickName;
      
       _playerInputa = GetComponent<PlayerInputController>();
       _characterController = GetComponent<CharacterController>();

       _cameraTransform = Camera.main.transform;
       canMove = _isIdle = true;
      
   }

   private void Update()
   {
       if (GetComponent<PhotonView>().IsMine)
       {
             MovePlayer();
       }
       
       if (isNearCube && Input.GetKeyDown(KeyCode.E))
       {
           Debug.Log("E tuşuna basıldı ve küpe yakın");
           GameObject lightObject = GameObject.Find("LightObjectName"); // Işık nesnesinin adını buraya yazın
           if (lightObject != null)
           {
               PhotonView photonView = lightObject.GetComponent<PhotonView>();
               if (photonView != null)
               {
                   Debug.Log("PhotonView bulundu ve RPC çağrısı yapılacak");
                   photonView.RPC("ChangeLightColor", RpcTarget.All);
               }
               else
               {
                   Debug.LogError("PhotonView bulunamadı");
               }
           }
           else
           {
               Debug.LogError("LightObjectName adında nesne bulunamadı");
           }
       }
      
   }


   void MovePlayer()
   {
       _groundedPlayer = _characterController.isGrounded;

            if (_groundedPlayer && _playerVelocity.y < 0) _playerVelocity.y = 0f;

            var moveInput = _playerInputa.GetMovement();
            var sprinted = _playerInputa.SprintedThisFrame();
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
   
   
   private void OnTriggerEnter(Collider other)
   {
       if (other.gameObject.CompareTag("Cube"))
       {
           isNearCube = true;
           Debug.Log("triggerlandı");
       }
   }

   private void OnTriggerExit(Collider other)
   {
       if (other.gameObject.CompareTag("Cube"))
       {
           isNearCube = false;
       }
   }
   
}