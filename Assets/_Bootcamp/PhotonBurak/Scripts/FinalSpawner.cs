using Photon.Pun;
using UnityEngine;

namespace _Bootcamp.PhotonBurak.Scripts
{
    public class FinalSpawner:MonoBehaviourPun
    {
        void Start()
        {
            Vector3 leftPosition = new Vector3(8.54f,5.02f,40.11f);
            Vector3 rightPosition = new Vector3(17.28f, 5.02f, 40.11f);

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
}