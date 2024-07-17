using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
     void Start()
    {
        
        NetworkManager.Singleton.NetworkConfig.ConnectionApproval = true;

        if (_Bootcamp.Scripts.LobbyCode.GameFramework.Manager.RelayManager.Instance.IsHost)
        {
            NetworkManager.Singleton.ConnectionApprovalCallback = ConnectionApproval;
            
            (byte[] allocationId, byte[] key, byte[] connectionData, string ip, int port) = _Bootcamp
                .Scripts.LobbyCode.GameFramework.Manager.RelayManager.Instance.GetHostConnectionInfo();
            
            
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetHostRelayData(ip,(ushort)port, allocationId, key, connectionData, true);
            NetworkManager.Singleton.StartHost();
        }
        else
        {
            (byte[] allocationId, byte[] key, byte[] connectionData, byte[] hostConnectionData, string ip, int port) = _Bootcamp
                .Scripts.LobbyCode.GameFramework.Manager.RelayManager.Instance.GetClientConnectionInfo();

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetClientRelayData(ip, (ushort)port, allocationId, key, connectionData, hostConnectionData, true);
            NetworkManager.Singleton.StartClient();
        }
    }

    private void ConnectionApproval(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
    {
        Debug.Log($"Player connection: {request.ClientNetworkId}");
        response.Approved = true;
        response.CreatePlayerObject = true;
        response.Pending = false;
    }
}
