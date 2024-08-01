using Photon.Pun;
using UnityEngine;

namespace _Bootcamp.PhotonBurak.Scripts
{
    public class RecepitonSpawner:MonoBehaviourPun
    {
        void Start()
        {
            Vector3 leftPosition = new Vector3(12.39f, 4.92f, 39.87f);
            Vector3 rightPosition = new Vector3(12.39f, 4.92f, 26.65f);

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