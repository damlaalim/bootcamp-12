using _Bootcamp.Scripts.Player;
using Cinemachine;
using UnityEngine;
using Zenject;

namespace _Bootcamp.Scripts.CameraSystem
{
    public class CinemachinePovExtension : CinemachineExtension
    {
        [SerializeField] 
        private float _clampAngle = 80, _horizontalSpeed = 10, _verticalSpeed = 10;

        [Inject] private PlayerInputController _inputController;
        
        private Vector3 _startingRot;

        protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (!vcam.Follow || stage != CinemachineCore.Stage.Aim || _inputController is null) return;
            if (_startingRot == null)
                _startingRot = transform.localRotation.eulerAngles;
            
            var deltaInput = _inputController.GetLookDelta();
            _startingRot.x += deltaInput.x * _verticalSpeed   * Time.deltaTime;
            _startingRot.y += deltaInput.y * _horizontalSpeed * Time.deltaTime;
            _startingRot.y = Mathf.Clamp(_startingRot.y, -_clampAngle, _clampAngle);
            state.RawOrientation = Quaternion.Euler(-_startingRot.y, _startingRot.x, 0f);
        }
    }
}