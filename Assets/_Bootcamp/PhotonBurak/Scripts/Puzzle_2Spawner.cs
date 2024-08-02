using Photon.Pun;
using UnityEngine;

namespace _Bootcamp.PhotonBurak.Scripts
{
    public class Puzzle_2Spawner: MonoBehaviourPun
    {
        void Start()
        {
            Vector3 leftPosition = new Vector3(1.30999994f, 0.0119304657f, -5.05999994f);
            Vector3 rightPosition = new Vector3(11.5699997f, 0.0119304657f, -5.05999994f);

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