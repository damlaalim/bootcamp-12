using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Photon.Pun;

public class LightSequenceManager : MonoBehaviourPunCallbacks
{
    public List<LightControl> lights;
    public List<int> correctSequence;

    private int currentOrder = 0;
  
    public void SwitchCheck(int order)
    {
        photonView.RPC("SwitchCheckRPC", RpcTarget.AllBuffered, order);
    }

    [PunRPC]
    private void SwitchCheckRPC(int order)
    {
        if (correctSequence[currentOrder] == order)
        {
            Debug.Log($"switch pull, id: {order}");
            currentOrder++;
            if (currentOrder >= correctSequence.Count)
            {
                Debug.Log("WIN");
            }
        }
        else
        {
            Debug.Log($"Yanlış switch, id: {order}");
            currentOrder = 0;
            ResetAllSwitches();
        }
    }

    void ResetAllSwitches()
    {
        Debug.Log("SIFIRLA");
        foreach (var light in lights)
        {
            light.TurnOff();
        }
    }

    public IEnumerator LightRoutine()
    {
         OpenAllSwitch();
         yield return new WaitForSeconds(2f);
         ResetAllSwitches();
         yield return new WaitForSeconds(0.3f);
         OpenAllSwitch();
         yield return new WaitForSeconds(0.6f);
         ResetAllSwitches();
         yield return new WaitForSeconds(0.3f);
         OpenAllSwitch();
         yield return new WaitForSeconds(0.1f);
         ResetAllSwitches();

    }
    
    void OpenAllSwitch()
    {
        foreach (var light in lights)
        {
            light.TurnOn();
        }
    }
}