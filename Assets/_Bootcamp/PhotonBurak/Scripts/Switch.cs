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
    
    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }
    
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
}