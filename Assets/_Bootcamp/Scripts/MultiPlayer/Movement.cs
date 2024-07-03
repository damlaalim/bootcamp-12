using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Movement : NetworkBehaviour
{
    public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();
    public float speed = 5f;

    private void Start()
    {
        if (IsOwner)
        {
            Move();
        }
    }

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            Move();
        }
    }

    void Update()
    {
        if (IsOwner)
        {
            HandleMovement();
        }
        else
        {
            transform.position = Position.Value;
        }
    }

    private void HandleMovement()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        transform.position += move * speed * Time.deltaTime;
        Position.Value = transform.position;
    }

    public void Move()
    {
        if (NetworkManager.Singleton.IsServer)
        {
            var randomPosition = GetRandomPositionOnPlane();
            transform.position = randomPosition;
            Position.Value = randomPosition;
        }
        else
        {
            submitPositionRequestServerRpc();
        }
    }

    [ServerRpc]
    void submitPositionRequestServerRpc(ServerRpcParams rpcParams = default)
    {
        var randomPosition = GetRandomPositionOnPlane();
        Position.Value = randomPosition;
    }

    static Vector3 GetRandomPositionOnPlane()
    {
        return new Vector3(Random.Range(-3f, 3f), 1f, Random.Range(-3f, 3f));
    }
}
