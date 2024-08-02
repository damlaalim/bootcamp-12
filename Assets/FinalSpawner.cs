using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalSpawner : MonoBehaviourPun
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 leftPosition = new Vector3(12.2600002f, 5.34200001f, 47.8899994f);
        Vector3 rightPosition = new Vector3(16.7000008f, 5.34200001f, 47.8899994f);

        Vector3 spawnPosition;
        if (PhotonNetwork.IsMasterClient)
        {
            spawnPosition = rightPosition;
        }
        else
        {
            spawnPosition = leftPosition;
        }

        PhotonNetwork.Instantiate("Player", spawnPosition, Quaternion.identity);
    }

}
