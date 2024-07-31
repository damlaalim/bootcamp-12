using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Photon.Pun;
using UnityEngine;

public class LightControl : MonoBehaviour
{
    public Light lightObject;

    private void Start()
    {
        TurnOff();
    }

    public void SetLightState(bool state)
    {
        lightObject.intensity = state ? 300 : 0;
        // lightObject.enabled = state;
    }
    public void TurnOff()
    {
        if (lightObject != null)
        {
            lightObject.intensity = 300;
            lightObject.DOIntensity(0,.2f);
            
        }
    } 
    public void TurnOn()
    {
        if (lightObject != null)
        {
            lightObject.intensity = 0;
            lightObject.DOIntensity(300,.2f);
        }
    }
    
}
