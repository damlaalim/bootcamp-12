using Photon.Pun;
using UnityEngine;

namespace _Bootcamp.PhotonBurak.Scripts
{
    public class Puzzle_2Spawner: MonoBehaviourPun
    {
        void Start()
        {
            Vector3 leftPosition = new Vector3(0,1,0);
            Vector3 rightPosition = new Vector3(10, 6, 20);

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