using Photon.Pun;
using UnityEngine;

public class DeadSpawner : MonoBehaviour
{
    Vector3 leftPosition = new Vector3(1.45129848f, 15.4154911f, 23.6990147f);
    Vector3 rightPosition = new Vector3(-13.82f, 6.3f, 13.43f);

    public Vector3 GetPosition()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            return rightPosition;
        }
        else
            return leftPosition;
    }
    
    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         Vector3 spawnPosition;
    //         if (PhotonNetwork.IsMasterClient)
    //         {
    //             Debug.Log("Right Position");
    //             spawnPosition = rightPosition;
    //         }
    //         else
    //         {
    //             Debug.Log("Left Position");
    //             spawnPosition = leftPosition;
    //         }
    //
    //         PhotonView otherPhotonView = other.GetComponent<PhotonView>();
    //         if (otherPhotonView != null && otherPhotonView.IsMine)
    //         {
    //             // Karakterin yeni pozisyona taşınması
    //             otherPhotonView.RPC("Teleport", RpcTarget.All, other.transform,spawnPosition);
    //         }
    //
    //     }
    //
    // }
    // [PunRPC]
    // private void Teleport(Transform transform, Vector3 spawnPosition)
    // {
    //     transform.transform.position = spawnPosition;
    //
    // }
}
