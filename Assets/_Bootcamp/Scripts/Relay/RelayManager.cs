using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay.Models;
using Unity.Services.Relay;
using UnityEngine;
using System.Threading.Tasks;
using Unity.Netcode.Transports.UTP;
using Unity.Netcode;
using UnityEngine.UI;

public class RelayManager : MonoBehaviour
{
    private string PlayerID;

    public TextMeshProUGUI idText;

    public TMP_Dropdown playerCount;

    private RelayHostData _data;

    private RelayJoinData _joindata;

    public TMP_InputField inputField;

    public TextMeshProUGUI JoinCodeText;


    async void Start()
    {
        await UnityServices.InitializeAsync();
        Debug.Log("Unity Service Init.");
        SignIn();
        
    }

    async void SignIn()
    {

        Debug.Log("Signing in");
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        PlayerID = AuthenticationService.Instance.PlayerId;
        Debug.Log("Signed in:"+PlayerID);
        idText.text = PlayerID;
    }

    public async void OnHostClickAsync()
    {

        int MaxplayerCount = Convert.ToInt32(this.playerCount.options[playerCount.value].text);

        Allocation allocation = await RelayService.Instance.CreateAllocationAsync(MaxplayerCount);

        _data = new RelayHostData()
        {
            IPv4Address = allocation.RelayServer.IpV4,
            Port = (ushort)allocation.RelayServer.Port,

            AllocationID = allocation.AllocationId,
            AllocationIDBytes = allocation.AllocationIdBytes,
            ConnectionData = allocation.ConnectionData,
            Key = allocation.Key,
        };

        _data.joinCode = await RelayService.Instance.GetJoinCodeAsync(_data.AllocationID);

        Debug.Log("allocation completed" + _data.AllocationID);

        Debug.Log("Join Code" + _data.joinCode);

        JoinCodeText.text = _data.joinCode;


        UnityTransport transport = NetworkManager.Singleton.gameObject.GetComponent<UnityTransport>();

        transport.SetRelayServerData(_data.IPv4Address, _data.Port, _data.AllocationIDBytes, _data.Key, _data.ConnectionData);

        NetworkManager.Singleton.StartHost();


    }
    public async void OnJoinClick()
    {
        JoinAllocation allocation = await RelayService.Instance.JoinAllocationAsync(inputField.text);

        _joindata = new RelayJoinData()
        {
            IPv4Address = allocation.RelayServer.IpV4,
            Port = (ushort)allocation.RelayServer.Port,
            AllocationID = allocation.AllocationId,
            AllocationIDBytes = allocation.AllocationIdBytes,
            ConnectionData = allocation.ConnectionData,
            HostConnectionData = allocation.HostConnectionData,
            Key = allocation.Key,
        };
        Debug.Log("JOİN success" + _joindata.AllocationID);

        UnityTransport transport = NetworkManager.Singleton.gameObject.GetComponent<UnityTransport>();

        transport.SetRelayServerData(_joindata.IPv4Address, _joindata.Port, _joindata.AllocationIDBytes, _joindata.Key, _joindata.ConnectionData, _joindata.HostConnectionData);
        NetworkManager.Singleton.StartClient();

    }

}
public struct RelayHostData
{
    public string joinCode;
    public string IPv4Address;
    public ushort Port;
    public Guid AllocationID;
    public byte[] AllocationIDBytes;
    public byte[] ConnectionData;
    public byte[] Key;
}
public struct RelayJoinData
{
    public string IPv4Address;
    public ushort Port;
    public Guid AllocationID;
    public byte[] AllocationIDBytes;
    public byte[] ConnectionData;
    public byte[] HostConnectionData;
    public byte[] Key;
}
