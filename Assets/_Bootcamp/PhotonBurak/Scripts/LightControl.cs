using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Photon.Pun;
using UnityEngine;

public class LightControl : MonoBehaviour
{
    public Light lightObject;

    
    public void SetLightState(bool state)
    {
        lightObject.intensity = 300;
        lightObject.enabled = state;
    }
    public void TurnOff()
    {
        if (lightObject != null)
        {
            lightObject.intensity = 300;
            lightObject.DOIntensity(0,.2f).OnComplete((() => lightObject.enabled = false));
            
        }
    } 
    public void TurnOn()
    {
        if (lightObject != null)
        {
            lightObject.intensity = 0;
            lightObject.enabled = true;
            lightObject.DOIntensity(300,.2f);
        }
    }
    
}
