using Photon.Pun;
using UnityEngine;

namespace _Bootcamp.PhotonBurak.Scripts
{
    public class OfficeSpawner: MonoBehaviourPun
    {
        void Start()
        {
            Vector3 leftPosition = new Vector3(-9.22f, 1.76f, -19.1f);
            Vector3 rightPosition = new Vector3(3.67f, 1.76f, -19.1f);

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