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
        string message = PhotonNetwork.NickName + " : " + inputField.text;
        photonView.RPC("ReceiveMessage", RpcTarget.All, message, Content);

        inputField.text = "";
    }

    [PunRPC]
    public void ReceiveMessage(string message, Transform content)
    {
        var messageObject = Instantiate(MessagePhoton, Vector3.zero, Quaternion.identity, content);
        messageObject.GetComponent<MessagePhoton>().MyMessage.text = message;
    }
}
