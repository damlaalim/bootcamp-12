using Unity.Netcode;
using UnityEngine;
using UnityEngine.Rendering;

namespace Game
{
    public class PlayerSpawnManager: MonoBehaviour
    {
       
        public AYellowpaper.SerializedCollections.SerializedDictionary<int, PlayerPositions> spawnpoints;
        void Start()
        {
            if (NetworkManager.Singleton.IsServer)
            {
                NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
                Debug.Log("start");
                
                if(NetworkManager.Singleton.IsHost)
                {
                    OnClientConnected(NetworkManager.Singleton.LocalClientId);
                    Debug.Log("onclient");
                }
            }
        }

        private void OnClientConnected(ulong clientId)
        {
            NetworkObject playerNetworkObject = NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject;
            Vector3 spawnPosition = GetSpawnPosition();
            SetPlayerPositionServerRpc(playerNetworkObject.NetworkObjectId, spawnPosition);
            Debug.Log(""+playerNetworkObject.transform.name);
        }

        private Vector3 GetSpawnPosition()
        {
            if (NetworkManager.Singleton.IsHost)
            {
                return spawnpoints[0].player1;
            }
            else
            {
                return spawnpoints[0].player2;

            }
        }

        [ServerRpc(RequireOwnership = false)]
        private void SetPlayerPositionServerRpc(ulong playerNetworkObjectId, Vector3 position, ServerRpcParams serverRpcParams = default)
        {
            NetworkObject playerNetworkObject = NetworkManager.Singleton.SpawnManager.SpawnedObjects[playerNetworkObjectId];
            playerNetworkObject.transform.position = position;
            SetPlayerPositionClientRpc(playerNetworkObjectId, position);
            Debug.Log("Position set on server");
        }
        
        [ClientRpc]
        private void SetPlayerPositionClientRpc(ulong playerNetworkObjectId, Vector3 position)
        {
            NetworkObject playerNetworkObject = NetworkManager.Singleton.SpawnManager.SpawnedObjects[playerNetworkObjectId];
            playerNetworkObject.transform.position = position;
            Debug.Log("Position set on client");
        }
        
        void OnDestroy()
        {
            if (NetworkManager.Singleton != null)
            {
                NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
            }
        }
        
    }
}