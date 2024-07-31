using Photon.Pun;
using UnityEngine;

namespace _Bootcamp.PhotonBurak.Scripts
{
    public class Puzzle_2Spawner: MonoBehaviourPun
    {
        void Start()
        {
            Vector3 leftPosition = new Vector3(+0.98f, 0.14f, -8.34f);
            Vector3 rightPosition = new Vector3(+11.34f, 0.14f, -8.34f);

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