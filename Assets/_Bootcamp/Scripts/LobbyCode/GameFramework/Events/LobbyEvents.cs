using Unity.Services.Lobbies.Models;

namespace _Bootcamp.Scripts.LobbyCode.GameFramework.Events
{
    public  static class LobbyEvents
    {
        public delegate void LobbyUpdated(Lobby lobby);

        public static LobbyUpdated OnLobbyUpdated;
    }
}