using UnityEngine;
using Photon.Pun;

public class Switch : MonoBehaviourPunCallbacks
{
    public LightControl lightControl; 
    public LightSequenceManager sequenceManager; 
    private bool isPlayerNearby = false;

    
    public int switchOrder = 0;
    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            photonView.RPC("ToggleSwitch", RpcTarget.AllBuffered); 
            ToggleSwitch(); 
            sequenceManager.SwitchCheck(switchOrder);
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
    
    void ToggleSwitch()
    {
        bool newState = !lightControl.lightObject.enabled;
        lightControl.SetLightState(newState);
    }
}