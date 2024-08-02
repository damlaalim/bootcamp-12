using _Bootcamp.Scripts.Interactable;
using UnityEngine;
using Photon.Pun;

public class Switch : MonoBehaviourPunCallbacks, IInteractable
{
    public Canvas _canvas;
    public LightControl lightControl; 
    public LightSequenceManager sequenceManager; 
    private bool isPlayerNearby = false;

    public int switchOrder = 0;
    
    [PunRPC]
    void ToggleSwitch()
    {
        // bool newState = !lightControl.lightObject.enabled;
        lightControl.SetLightState(true);
    }

    public Canvas ICanvas { get => _canvas; set{} }
    public void Do()
    {
        photonView.RPC("ToggleSwitch", RpcTarget.AllBuffered); 
        sequenceManager.SwitchCheck(switchOrder);
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