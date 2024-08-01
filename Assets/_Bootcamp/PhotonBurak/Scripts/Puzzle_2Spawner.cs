using Photon.Pun;
using UnityEngine;

namespace _Bootcamp.PhotonBurak.Scripts
{
    public class Puzzle_2Spawner: MonoBehaviourPun
    {
        void Start()
        {
            Vector3 leftPosition = new Vector3(0f,0.06f,-7.15f);
            Vector3 rightPosition = new Vector3(11.17f, 0.06f, -7.15f);

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