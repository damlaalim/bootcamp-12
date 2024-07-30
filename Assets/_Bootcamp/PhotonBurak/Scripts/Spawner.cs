using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    void Start()
    {
        Vector3 leftPosition = new Vector3(+62.90118f, 80f, 198f);
        Vector3 rightPosition = new Vector3(-42f, -141f, 137f);

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
