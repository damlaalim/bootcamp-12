using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class ColorLamp : MonoBehaviourPunCallbacks
{
    public Light targetLight;

    [PunRPC]
    public void ChangeLightColor()
    {
        Debug.Log("ChangeLightColor RPC çağrıldı");
        targetLight.color = Color.blue;
       
    }
}
