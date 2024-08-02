using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class ShieldOnFloor : MonoBehaviourPunCallbacks
{
    public static ShieldOnFloor Instance;
    public GameObject lightPointOnWall;

    [SerializeField] private GameObject _lightPointOnFloor;

    private void Awake()
    {
        Instance = this;
    }

    public void PlaceLightPoint()
    {
        photonView.RPC("PlaceLightPointRPC", RpcTarget.AllBuffered);
    }

    public void DestroyLightPoint()
    {
        photonView.RPC("DestroyLightPointRPC", RpcTarget.AllBuffered);
    }

    [PunRPC]
    private void PlaceLightPointRPC()
    {
        lightPointOnWall.SetActive(true);
    }
    
    [PunRPC]
    private void DestroyLightPointRPC()
    {
        _lightPointOnFloor.SetActive(false);
    }
}