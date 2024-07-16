using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        // NetworkManager.Singleton'ın null olup olmadığını kontrol edin
        if (NetworkManager.Singleton == null)
        {
            Debug.LogError("NetworkManager.Singleton is null. Make sure there is a NetworkManager component in the scene.");
            return;
        }

        // UnityTransport bileşeninin null olup olmadığını kontrol edin
        UnityTransport transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
        if (transport == null)
        {
            Debug.LogError("UnityTransport component is not attached to the NetworkManager.");
            return;
        }

        NetworkManager.Singleton.NetworkConfig.ConnectionApproval = true;

        if (_Bootcamp.Scripts.LobbyCode.GameFramework.Manager.RelayManager.Instance.IsHost)
        {
            NetworkManager.Singleton.ConnectionApprovalCallback = ConnectionApproval;
            (byte[] allocationId, byte[] key, byte[] connectionData, string ip, int port) = _Bootcamp
                .Scripts.LobbyCode.GameFramework.Manager.RelayManager.Instance.GetHostConnectionInfo();

            transport.SetHostRelayData(ip, (ushort)port, allocationId, key, connectionData, true);
            NetworkManager.Singleton.StartHost();
        }
        else
        {
            (byte[] allocationId, byte[] key, byte[] connectionData, byte[] hostConnectionData, string ip, int port) = _Bootcamp
                .Scripts.LobbyCode.GameFramework.Manager.RelayManager.Instance.GetClientConnectionInfo();

            transport.SetClientRelayData(ip, (ushort)port, allocationId, key, connectionData, hostConnectionData, true);
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
