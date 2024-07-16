using _Bootcamp.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class ButtonsActions : MonoBehaviour
{

    private NetworkManager NetworkManager;
    void Start()
    {
        NetworkManager = GetComponentInParent<NetworkManager>();    
    }

    public void StartHost()
    {
        NetworkManager.StartHost();
        
    }

    public void StartClient()
    {
        NetworkManager.StartClient();
    }

 


}
