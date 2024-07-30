using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    void Start()
    {
      PhotonNetwork.Instantiate("Player", new Vector3(Random.Range(-3, +3), -1.5f, 0), Quaternion.identity);
    }
}