using Photon.Pun;
using UnityEngine;

public class DeadSpawner : MonoBehaviour
{
    Vector3 leftPosition = new Vector3(2.3927536f, 26.1755829f, 44.4430923f);
    Vector3 rightPosition = new Vector3(-28.8164616f, 7.12477493f, 27.9515495f);

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
