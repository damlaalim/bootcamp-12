using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using _Bootcamp.Scripts.LobbyCode.Game.Data;
using Game.Events;
using GameFramework.Core;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using LobbyEvents = _Bootcamp.Scripts.LobbyCode.GameFramework.Events.LobbyEvents;


namespace _Bootcamp.Scripts.LobbyCode.GameFramework.Manager
{
    public class LobbyManager : Singleton<LobbyManager>
    {
        
        
        private Lobby _lobby;
        private Coroutine _heartbeatcCorotine;
        private Coroutine _refreshLobbyCoroutine;

        public string GetLobbyCode()
        {
            return _lobby?.LobbyCode;
        }
        
        public async Task<bool> CreateLobby(int maxPlayers,bool isPrivate, Dictionary<string, string> data, Dictionary<string,string> lobbyData)
        {
            Dictionary<string, PlayerDataObject> playerData = SerializePlayerData(data);
            Unity.Services.Lobbies.Models.Player player = new Unity.Services.Lobbies.Models.Player(AuthenticationService.Instance.PlayerId, null, playerData);

            CreateLobbyOptions options = new CreateLobbyOptions()
            {
                Data = SerializeLobbyData(lobbyData),
                IsPrivate = isPrivate,
                Player = player,
            };

            try
            {
                _lobby = await LobbyService.Instance.CreateLobbyAsync("Lobby", maxPlayers, options);

            }
            catch (SystemException)
            {
                return false;
            }

                Debug.Log($"Lobby Created wtih in lobby id {_lobby.Id}");

                _heartbeatcCorotine = StartCoroutine(HeartbeatLobbyCorotine(_lobby.Id, 6f));
                _refreshLobbyCoroutine = StartCoroutine(RefreshLobbyCorotine(_lobby.Id, 1f));
                
                return true;
        }

        private IEnumerator HeartbeatLobbyCorotine(string lobbyId,float waitTimeSeconds)
        {
            while (true)
            {
                 Debug.Log("heartbeat");
                 LobbyService.Instance.SendHeartbeatPingAsync(lobbyId);
                 yield return new WaitForSecondsRealtime(waitTimeSeconds);
            }
        }
        
        private IEnumerator RefreshLobbyCorotine(string lobbyId,float waitTimeSeconds)
        {
            while (true)
            {
                Task<Lobby> task = LobbyService.Instance.GetLobbyAsync(lobbyId);
                yield return new WaitUntil(() => task.IsCompleted);
                Lobby newLobby = task.Result;

                if (newLobby.LastUpdated > _lobby.LastUpdated)
                {
                    _lobby = newLobby;
                    LobbyEvents.OnLobbyUpdated?.Invoke(_lobby);
                }
                yield return new WaitForSecondsRealtime(waitTimeSeconds);
            }
        }

        private Dictionary<string, PlayerDataObject> SerializePlayerData(Dictionary<string, string> data)
        {
            Dictionary<string, PlayerDataObject> playerData = new Dictionary<string, PlayerDataObject>();

            foreach (var (key, value) in data)
            {
                playerData.Add(key, new PlayerDataObject(
                    visibility: PlayerDataObject.VisibilityOptions.Member,
                    value: value));
            }
            
            return playerData;
        }
        
        private Dictionary<string, DataObject> SerializeLobbyData(Dictionary<string, string> data)
        {
            Dictionary<string, DataObject> LobbyData = new Dictionary<string, DataObject>();

            foreach (var (key, value) in data)
            {
                LobbyData.Add(key, new DataObject(
                    visibility: DataObject.VisibilityOptions.Member,
                    value: value));
            }
            
            return LobbyData;
        }

        public void OnApplicationQuit()
        {
            if (_lobby != null && _lobby.HostId == AuthenticationService.Instance.PlayerId)
            {
                LobbyService.Instance.DeleteLobbyAsync(_lobby.Id);
            }
        }


        public async Task<bool> JoinLobby(string code, Dictionary<string, string> playerData)
        {
            JoinLobbyByCodeOptions options = new JoinLobbyByCodeOptions();
            Unity.Services.Lobbies.Models.Player player =
                new Unity.Services.Lobbies.Models.Player(AuthenticationService.Instance.PlayerId, null,
                    SerializePlayerData(playerData));

            options.Player = player;
            try
            {
                    _lobby = await LobbyService.Instance.JoinLobbyByCodeAsync(code,options);
            }
            catch (SystemException)
            {
                return false;
            }
            _refreshLobbyCoroutine = StartCoroutine(RefreshLobbyCorotine(_lobby.Id, 1f));
            return true;
        }

        public List<Dictionary<string, PlayerDataObject>> GetPlayersData()
        {
            List<Dictionary<string, PlayerDataObject>> data = new List<Dictionary<string, PlayerDataObject>>();

            foreach (Unity.Services.Lobbies.Models.Player player in _lobby.Players )
            {
                data.Add(player.Data);
            }

            return data;
        }

        public async Task<bool> UpdatePlayerData(string playerId, Dictionary<string, string> data, string allocationId = default, string connectionData =default)
        {
            Dictionary<string, PlayerDataObject> playerData = SerializePlayerData(data);
            UpdatePlayerOptions options = new UpdatePlayerOptions()
            {
                Data = playerData,
                AllocationId = allocationId,
                ConnectionInfo = connectionData
            };

            try
            {
               _lobby = await LobbyService.Instance.UpdatePlayerAsync(_lobby.Id, playerId, options);

            }
            catch (System.Exception)
            {
                return false;
            }

            LobbyEvents.OnLobbyUpdated(_lobby);
            
            return true;
        }


        public async Task<bool> UpdateLobbyData(Dictionary<string,string> data)
        {
            Dictionary<string, DataObject> LobbyData = SerializeLobbyData(data);

            UpdateLobbyOptions options = new UpdateLobbyOptions()
            {
                Data = LobbyData
            };

            try
            {
                _lobby = await LobbyService.Instance.UpdateLobbyAsync(_lobby.Id, options);
                
            }
            catch (System.Exception)
            {
                return false;
            }

            LobbyEvents.OnLobbyUpdated(_lobby);
            
            return true;
        }
        

        public string GetHostId()
        {
            return _lobby.HostId;
        }
    }
}