using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SwitchOnGround : MonoBehaviourPunCallbacks
{
    public static SwitchOnGround Instance;
    public GameObject switchOnWall;

    [SerializeField] private GameObject switchOnGround;

    private void Awake()
    {
        Instance = this;
    }

    public void PlaceSwitch()
    {
        photonView.RPC("PlaceSwitchRPC", RpcTarget.AllBuffered);
    }

    public void DestroySwitch()
    {
        photonView.RPC("DestroySwitchRPC", RpcTarget.AllBuffered);
    }
    
    [PunRPC]
    private void PlaceSwitchRPC()
    {
        switchOnWall.SetActive(true);
    }

    [PunRPC]
    private void DestroySwitchRPC()
    {
        switchOnGround.SetActive(false);
    }
}