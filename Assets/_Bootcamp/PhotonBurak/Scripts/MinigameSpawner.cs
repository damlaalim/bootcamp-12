using Photon.Pun;
using UnityEngine;

namespace _Bootcamp.PhotonBurak.Scripts
{
    public class MinigameSpawner:MonoBehaviourPun
    {
        void Start()
        {
            Vector3 leftPosition = new Vector3(13.1f, 0.4f, 39.4f);
            Vector3 rightPosition = new Vector3(37.56f, 0.4f, 39.4f);

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