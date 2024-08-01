using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class TutorialSpawner : MonoBehaviourPun
{
    void Start()
    {
        Vector3 leftPosition = new Vector3(+62f, 80f, 198f);
        Vector3 rightPosition = new Vector3(-13.82f, 6.3f, 13.43f);

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
