using System;
using System.Collections.Generic;
using _Bootcamp.Scripts.LobbyCode.Game.Data;
using UnityEngine;
using Game.Events;

namespace Game
{
    public class LobbySpawner : MonoBehaviour
    {
        [SerializeField] private List<LobbyPlayer> _players;

        private void OnEnable()
        {
            LobbyEvents.OnLobbyUpdated += OnLobbyUpdated;
        }
        
        private void OnDisable()
        {
            LobbyEvents.OnLobbyUpdated -= OnLobbyUpdated;
        }

        private void OnLobbyUpdated()
        {
            List<LobbyPlayerData> playerDatas = GameLobbyManager.Instance.GetPlayers();

            if (playerDatas.Count > _players.Count)
            {
                Debug.LogWarning("Not enough LobbyPlayer objects to assign data.");
                return;
            }

            for (int i = 0; i < playerDatas.Count; i++)
            {
                LobbyPlayerData data = playerDatas[i];

                if (_players[i] != null)
                {
                    _players[i].SetData(data);
                }
                else
                {
                    Debug.LogWarning($"LobbyPlayer at index {i} is null.");
                }
            }
        }
    }
}