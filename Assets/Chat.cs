using System.Collections;
using System.Collections.Generic;
using _Bootcamp.PhotonBurak.Scripts;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Chat : MonoBehaviourPun
{
    public InputField inputField;
    public GameObject MessagePhoton;
    public GameObject Content;

    public void SendMessage()
    {
        Debug.Log("SendMessage called");
        photonView.RPC("GetMessage", RpcTarget.All, PhotonNetwork.NickName + " : " + inputField.text);

        inputField.text = "";
    }

    [PunRPC]
    public void GetMessage(string receiveMessage, PhotonMessageInfo info)
    {
        Debug.Log("GetMessage called with message: " + receiveMessage);

     
        Chat chatComponent = info.Sender.TagObject as Chat;

        if (chatComponent != null)
        {
            GameObject M = Instantiate(MessagePhoton, Vector3.zero, Quaternion.identity, chatComponent.Content.transform);
            M.GetComponent<MessagePhoton>().MyMessage.text = receiveMessage;
        }
    }

    private void Start()
    {
        // PhotonView'un TagObject özelliğine bu script'i ekleyelim
        if (photonView.IsMine)
        {
            photonView.Owner.TagObject = this;
        }
    }
}