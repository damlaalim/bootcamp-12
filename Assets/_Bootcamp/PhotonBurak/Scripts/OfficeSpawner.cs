using Photon.Pun;
using UnityEngine;

namespace _Bootcamp.PhotonBurak.Scripts
{
    public class OfficeSpawner: MonoBehaviourPun
    {
        void Start()
        {
            Vector3 leftPosition = new Vector3(11.16f, 0.27f, 48.63f);
            Vector3 rightPosition = new Vector3(6.76f, 0.27f, 48.93f);

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