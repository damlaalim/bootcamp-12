using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class TutorialSpawner : MonoBehaviourPun
{
    void Start()
    {
        Vector3 leftPosition = new Vector3(+0.92f, 11.28f, 16.05f);
        Vector3 rightPosition = new Vector3(-9.98f, 4.375f, 9.87f);

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
