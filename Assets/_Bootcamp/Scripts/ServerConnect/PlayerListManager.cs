using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Bootcamp.Scripts.ServerConnect
{
    public class PlayerListManager: MonoBehaviourPunCallbacks
    {
        public TextMeshProUGUI playerNamesText, roomNameText; 
        public List<string> playerNames = new List<string>();
        public Button playButton;

        public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
            photonView.RPC("UpdatePlayerList", RpcTarget.AllBuffered);
        }

        public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
        {
            base.OnPlayerLeftRoom(otherPlayer);
            photonView.RPC("UpdatePlayerList", RpcTarget.AllBuffered);
        }

        [PunRPC]
        private void UpdatePlayerList()
        {
            playerNames.Clear();
            foreach (var player in PhotonNetwork.PlayerList)
            {
                playerNames.Add(player.NickName);
            }

            playerNamesText.text = string.Join("\n", playerNames);
        }

        private void Start()
        {
            if (PhotonNetwork.InRoom)
            {
                photonView.RPC("UpdatePlayerList", RpcTarget.AllBuffered);
            }
            if (PhotonNetwork.CurrentRoom != null)
            {
                roomNameText.text = PhotonNetwork.CurrentRoom.Name;
            }
            playButton.interactable = PhotonNetwork.LocalPlayer.IsMasterClient;
        }
    }
}