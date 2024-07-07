using UnityEngine;

namespace _Bootcamp.Scripts.UI
{
    public class LookAtCamera: MonoBehaviour
    {
        private Camera _mainCam;
        private void Start()
        {
            _mainCam = Camera.main;
        }

        private void LateUpdate()
        {
            var mainCamRot = _mainCam.transform.rotation;
            
            transform.LookAt(transform.position + mainCamRot * Vector3.forward,
                mainCamRot * Vector3.up);
        }
    }
}