using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject playerPrefab; // Karakter prefab'inin referansı
    public Transform[] spawnPoints; // Karakterin spawn olacağı noktaların listesi
    private static int nextSpawnPointIndex = 0;

    void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            SpawnPlayer();
        }
    }

    void SpawnPlayer()
    {
        // Sıradaki spawn noktasını seçin
        Transform spawnPoint = spawnPoints[nextSpawnPointIndex];
        nextSpawnPointIndex = (nextSpawnPointIndex + 1) % spawnPoints.Length;

        // Karakteri belirlenen noktada spawn edin
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, spawnPoint.rotation);
    }
}
