using System.Collections.Generic;
using System.Threading.Tasks;
using _Bootcamp.Scripts.LobbyCode.Game.Data;
using _Bootcamp.Scripts.LobbyCode.GameFramework.Events;
using _Bootcamp.Scripts.LobbyCode.GameFramework.Manager;
using Unity.Services.Authentication;
using Unity.Services.Lobbies.Models;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;


namespace Game
{
     public class GameLobbyManager : GameFramework.Core.Singleton<GameLobbyManager>
     {
     
         private List<LobbyPlayerData> _lobbyPlayerDatas = new List<LobbyPlayerData>();
     
         private LobbyPlayerData _localLobbyPlayerData;

         private LobbyData _lobbyData;

         private int _maxNumberofPlayers = 4;
         private bool _inGame = false;
         
         
         public bool IsHost => _localLobbyPlayerData.Id == LobbyManager.Instance.GetHostId();
         private void OnEnable()
         {
             LobbyEvents.OnLobbyUpdated += OnLobbyUpdated;
         }
         
         private void OnDisable()
         {
             
             LobbyEvents.OnLobbyUpdated -= OnLobbyUpdated;
     
         }
     
     
         public string GetLobbyCode()
         {
             return LobbyManager.Instance.GetLobbyCode();
         }
        public async Task<bool> CreateLobby()
        {
            _localLobbyPlayerData= new LobbyPlayerData();
            _localLobbyPlayerData.Initialize(AuthenticationService.Instance.PlayerId, "HostPlayer");
            _lobbyData = new LobbyData();
            _lobbyData.Initialize(0);
           bool succeded = await LobbyManager.Instance.CreateLobby(_maxNumberofPlayers, true, _localLobbyPlayerData.Serialize(),_lobbyData.Serialize());
           return succeded;
        }
     
        
        public async Task<bool> JoinLobby(string code)
        {
            _localLobbyPlayerData = new LobbyPlayerData();
            _localLobbyPlayerData.Initialize(AuthenticationService.Instance.PlayerId, "JoinPlayer");
     
            bool succeded = await LobbyManager.Instance.JoinLobby(code, _localLobbyPlayerData.Serialize());
            return succeded;
        }
        
        private  async void OnLobbyUpdated(Lobby lobby)
        {
            List<Dictionary<string, PlayerDataObject>> playerData = LobbyManager.Instance.GetPlayersData();
            
            _lobbyPlayerDatas.Clear();

            int numberofPlayerReady = 0;
     
            foreach (Dictionary<string, PlayerDataObject> data in playerData)
            {
                LobbyPlayerData lobbyPlayerData = new LobbyPlayerData();
                lobbyPlayerData.Initialize(data);

                if (lobbyPlayerData.IsReady)
                {
                    numberofPlayerReady++;
                }
     
                if (lobbyPlayerData.Id == AuthenticationService.Instance.PlayerId)
                {
                    _localLobbyPlayerData = lobbyPlayerData;
                }
                
                _lobbyPlayerDatas.Add(lobbyPlayerData);
                
            }


            _lobbyData = new LobbyData();
            _lobbyData.Initialize(lobby.Data);
            
            Events.LobbyEvents.OnLobbyUpdated?.Invoke();

            if (numberofPlayerReady == lobby.Players.Count)
            {
                Events.LobbyEvents.OnLobbyReady?.Invoke();
            }


            if (_lobbyData.RelayJoinCode != default && !_inGame)
            {
                await JoinRelayService(_lobbyData.RelayJoinCode);
                SceneManager.LoadSceneAsync(_lobbyData.SceneName);
            }
            
            
        }

        

        public List<LobbyPlayerData> GetPlayers()
        {
            return _lobbyPlayerDatas;
        }

        public async Task<bool> SetPlayerReady()
        {
            _localLobbyPlayerData.IsReady = true;
            return await LobbyManager.Instance.UpdatePlayerData(_localLobbyPlayerData.Id,
                _localLobbyPlayerData.Serialize());
        }

        public int GetMapIndex()
        {
            return _lobbyData.MapIndex;
        }

        public async Task<bool> SetSelectedMap(int currentMapIndex, string sceneName)
        {
            _lobbyData.MapIndex = currentMapIndex;
            _lobbyData.SceneName = sceneName;
                
            return await LobbyManager.Instance.UpdateLobbyData(_lobbyData.Serialize());
        }

        public async Task StartGame()
        {
            string RelayJoinCode =
                await _Bootcamp.Scripts.LobbyCode.GameFramework.Manager.RelayManager.Instance.CreateRelay(
                    _maxNumberofPlayers);
            _inGame = true;
            _lobbyData.RelayJoinCode = RelayJoinCode;

            await LobbyManager.Instance.UpdateLobbyData(_lobbyData.Serialize());


            string allocationId =
                _Bootcamp.Scripts.LobbyCode.GameFramework.Manager.RelayManager.Instance.GetAllocationId();
            string connectionData = _Bootcamp.Scripts.LobbyCode.GameFramework.Manager.RelayManager.Instance
                .GetConnectionData();

            await LobbyManager.Instance.UpdatePlayerData(_localLobbyPlayerData.Id, _localLobbyPlayerData.Serialize(), allocationId, connectionData);

            SceneManager.LoadScene(_lobbyData.SceneName);
        }
        
        private async Task<bool> JoinRelayService(string relayJoinCode)
        {

            _inGame = true;
            await _Bootcamp.Scripts.LobbyCode.GameFramework.Manager.RelayManager.Instance.JoinRelay(relayJoinCode);
            
            string allocationId =
                _Bootcamp.Scripts.LobbyCode.GameFramework.Manager.RelayManager.Instance.GetAllocationId();
            string connectionData = _Bootcamp.Scripts.LobbyCode.GameFramework.Manager.RelayManager.Instance
                .GetConnectionData();

            await LobbyManager.Instance.UpdatePlayerData(_localLobbyPlayerData.Id, _localLobbyPlayerData.Serialize(), allocationId, connectionData);
            
            return true;
        }
     }
}

