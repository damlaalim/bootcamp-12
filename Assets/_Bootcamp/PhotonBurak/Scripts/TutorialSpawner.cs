using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class TutorialSpawner : MonoBehaviourPun
{
    void Start()
    {
        Vector3 leftPosition = new Vector3(1.97714996f, 27.8309822f, 48.0616913f);
        Vector3 rightPosition = new Vector3(-30.0397682f, 7.03725433f, 29.3938522f);

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
