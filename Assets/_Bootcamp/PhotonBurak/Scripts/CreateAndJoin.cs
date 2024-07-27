using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using Photon.Realtime;

public class CreateAndJoin : MonoBehaviourPunCallbacks
{
   public TMP_InputField input_Create;
   public TMP_InputField _input_join;

   public void CreateRoom()
   {
       PhotonNetwork.CreateRoom(input_Create.text);
   }

   public void JoinRoom()
   {
       PhotonNetwork.JoinRoom(_input_join.text);

   }

   public void JoinRoomInList(string roomName)
   {
       PhotonNetwork.JoinRoom(roomName);
   }

   public override void OnJoinedRoom()
   {
       PhotonNetwork.LoadLevel("Office");
   }
   
   
   
}
