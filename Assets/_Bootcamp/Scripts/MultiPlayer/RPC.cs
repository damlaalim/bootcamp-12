using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class RPC : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        if (!IsServer)
        {
            TestServerRpc(0);
        }
    }

    [ClientRpc]
    void TestClientRpc(int value)
    {
        Debug.Log("Client received the RPC with value: " + value);
        if (IsClient)
        {
            TestServerRpc(value + 1);
        }
    }

    [ServerRpc]
    void TestServerRpc(int value)
    {
        Debug.Log("Server received the RPC with value: " + value);
        TestClientRpc(value);
    }
}
