using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class LightControl : MonoBehaviour
{
    public Light lightObject;
    public int lightOrder; 

    private void Start()
    {
        lightObject.enabled = false;
    }

    public void SetLightState(bool state)
    {
        lightObject.enabled = state;
    }
}
