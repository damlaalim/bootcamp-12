using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class TutorialSpawner : MonoBehaviourPun
{
    void Start()
    {
        Vector3 leftPosition = new Vector3(+6.3f, 91.3f, 159.5f);
        Vector3 rightPosition = new Vector3(-97.8f, 19.04f, 95.5f);

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
