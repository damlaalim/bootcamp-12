using UnityEngine;

namespace _Bootcamp.Scripts.Interactable
{
    public class InteractableController : MonoBehaviour, IInteractable
    {
        [SerializeField] private Canvas _canvas;
        
        public Canvas ICanvas
        {
            get => _canvas;
            set { }
        }

        public void Do()
        {
            Debug.Log($"Interaction, obj: {gameObject.name}", gameObject);
            if (gameObject.tag == "Lamp" || gameObject.tag == "Mirror")
            {
                RotateObjects rotateObjectComponent = gameObject.GetComponent<RotateObjects>();
                if (rotateObjectComponent != null)
                {
                    rotateObjectComponent.RotateObject();
                }
            }
            if (gameObject.tag == "LightPoint")
            {
                ShieldOnFloor.Instance.PlaceLightPoint();
                ShieldOnFloor.Instance.DestroyLightPoint();
            }
            if (gameObject.tag == "SwitchOnFloor")
            {
                SwitchOnGround.Instance.PlaceSwitch();
                SwitchOnGround.Instance.DestroySwitch();
            }
            if (gameObject.tag == "SwitchOnWall")
            {
                SwitchOnWall.Instance.WallSwitch();
            }
        }

        public void ShowCanvas(bool show)
        {
            ICanvas.enabled = show;
        }

        public void ChangePos(Camera mainCam)
        {
            var mainCamRot = mainCam.transform.rotation;
            _canvas.transform.LookAt(_canvas.transform.position + mainCamRot * Vector3.forward,
                mainCamRot * Vector3.up);
        }
    }
}