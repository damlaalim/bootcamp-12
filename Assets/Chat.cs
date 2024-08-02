using System;
using System.Collections;
using System.Collections.Generic;
using _Bootcamp.PhotonBurak.Scripts;
using UnityEngine;
using Photon.Pun;

using UnityEngine.UI;

public class Chat : MonoBehaviourPunCallbacks
{
    public InputField inputField;
    public GameObject MessagePhoton;
    public GameObject Content;

    public void SendMessage()
    {
        photonView.RPC("GetMessage",RpcTarget.All,(PhotonNetwork.NickName + " : " + inputField.text));

        inputField.text = "";
    }

    [PunRPC]
    public void GetMessage(string receiveMessage)
    {
        GameObject M = Instantiate(MessagePhoton, Vector3.zero, Quaternion.identity, Content.transform);
        M.GetComponent<MessagePhoton>().MyMessage.text = receiveMessage;
    }
    
    
}
