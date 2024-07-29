using _Bootcamp.Scripts.LobbyCode.GameFramework.Network.Movement;
using Cinemachine;
using Unity.Netcode;
using UnityEngine;
using Cursor = UnityEngine.Cursor;

namespace _Bootcamp.Scripts.LobbyCode.Game
{
    [RequireComponent(typeof(CharacterController))]

    public class PlayerController : NetworkBehaviour
    {

    
        [SerializeField] private Vector2 _minMaxRotationX;
        [SerializeField] private Transform _camTransform;
        [SerializeField] private NetworkMovementComponent _playerMovement;
    
        private CharacterController _cc;
        private PlayerControl _playerControl;
        private float cameraAngle;

    
        public override void OnNetworkSpawn()
        {
            CinemachineVirtualCamera cvm = _camTransform.gameObject.GetComponent<CinemachineVirtualCamera>();


            if (IsOwner)
            {
                cvm.Priority = 1;
            }
            else
            {
                cvm.Priority = 0;
            }


        }
    
        void Start()
        {
            _cc = GetComponent<CharacterController>();

            _playerControl = new PlayerControl();
            _playerControl.Enable();
        
            Cursor.lockState = CursorLockMode.Locked;
        }


        void Update()
        {
            Vector2 movementInput = _playerControl.Player.Move.ReadValue<Vector2>();
            Vector2 lookInput = _playerControl.Player.Look.ReadValue<Vector2>();

            if (IsClient && IsLocalPlayer)
            {
                _playerMovement.ProcessLocalPlayerMovement(movementInput, lookInput);
            }else
            {
                _playerMovement.ProcessSimulatorPlayerMovement();
            }
        }
    
    
        /*private void RotateCamera(Vector2 lookInput)
    {

        cameraAngle = Vector3.SignedAngle(transform.forward, _camTransform.forward,_camTransform.right);
        float rotationAmount = lookInput.y * _turnspeed * Time.deltaTime;
        float newCameraAngle = cameraAngle - rotationAmount;

        if (newCameraAngle <= _minMaxRotationX.x && newCameraAngle >= _minMaxRotationX.y)
        {
             _camTransform.RotateAround(_camTransform.position,_camTransform.right, -lookInput.y* _turnspeed *Time.deltaTime);
        }

    }*/
    

    
    
    }
}
