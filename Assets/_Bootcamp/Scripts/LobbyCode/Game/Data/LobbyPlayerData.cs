using System.Collections.Generic;
using Unity.Services.Lobbies.Models;

namespace _Bootcamp.Scripts.LobbyCode.Game.Data
{
    public class LobbyPlayerData
    {
        private string _id;
        private string _gamertag;
        private bool _isready;

        public string Id
        {
            get => _id;
        }

        public string GamerTag => _gamertag;

        public bool IsReady
        {
            get => _isready;
            set => _isready = value;
        }
        
        
        public void Initialize(string id, string gamertag)
        {
            _id = id;
            _gamertag = gamertag;
        }

        public void Initialize(Dictionary<string, PlayerDataObject> playerData)
        {
            UpdateState(playerData);
            
        }

        public void UpdateState(Dictionary<string, PlayerDataObject> playerData)
        {
            if (playerData.ContainsKey("Id"))
            {
                _id = playerData["Id"].Value;
            }
            if (playerData.ContainsKey("Gamertag"))
            {
                _gamertag = playerData["Gamertag"].Value;
            }
            if (playerData.ContainsKey("isReady"))
            {
                _isready = playerData["isReady"].Value == "True";
            }
        }
        

        public Dictionary<string, string> Serialize()
        {
            return new Dictionary<string, string>()
            {
                {"Id", _id},
                {"Gamertag",_gamertag},
                {"isReady", _isready.ToString()},
            };
        }
        

    }
}