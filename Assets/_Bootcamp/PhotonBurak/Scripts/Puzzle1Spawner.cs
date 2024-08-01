using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Puzzle1Spawner : MonoBehaviourPun
{
    void Start()
    {
        Vector3 leftPosition = new Vector3(14.78f, 5.49f, 39.25f);
        Vector3 rightPosition = new Vector3(14.78f, 5.49f, 21.34f);

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
