using _Bootcamp.Scripts.Player;
using Cinemachine;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace _Bootcamp.Scripts.CameraSystem
{
    public class CinemachinePovExtension : CinemachineExtension
    {
        [SerializeField] private PhotonCharacterController _photonCharacterController;
        [SerializeField] private float _clampAngle = 80, _horizontalSpeed = 10, _verticalSpeed = 10;
        [SerializeField] private PlayerInputController _inputController;
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private Transform _playerBody;

        private Vector3 _startingRot;

        protected override void OnEnable()
        {
            base.OnEnable();
            _startingRot = transform.localRotation.eulerAngles;
        }

        protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (!vcam.Follow || stage != CinemachineCore.Stage.Aim || _inputController is null|| !_photonCharacterController.IsMine) return;

            var deltaInput = _photonCharacterController.canMove?_inputController.GetLookDelta(): Vector2.zero;
        
            _startingRot.x += deltaInput.x * _horizontalSpeed * deltaTime;
            _startingRot.y += deltaInput.y * _verticalSpeed * deltaTime;

            _startingRot.x = NormalizeAngle(_startingRot.x);
            _startingRot.y = Mathf.Clamp(_startingRot.y, -_clampAngle, _clampAngle);
            
            state.RawOrientation = Quaternion.Euler(-_startingRot.y, _startingRot.x, 0f);

            _playerBody.localRotation = Quaternion.Euler(0f, _startingRot.x, 0f);
            
            _photonCharacterController.photonView.RPC("SyncRotation", RpcTarget.Others, _playerBody.localRotation);

        }

        private float NormalizeAngle(float angle)
        {
            while (angle > 360f) angle -= 360f;
            while (angle < 0f) angle += 360f;
            return angle;
        }
    }
}