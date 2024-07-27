using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject playerPrefab; // Karakter prefab'inin referansı
    public Transform spawnPoint; // Karakterin spawn olacağı nokta
    void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, spawnPoint.rotation);
        }
        
    }

    
}
