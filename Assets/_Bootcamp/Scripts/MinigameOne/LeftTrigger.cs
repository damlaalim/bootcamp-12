using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class LeftTrigger : MonoBehaviourPunCallbacks
{
    public Animator fenceAnim;
    public Animator wallAnim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            photonView.RPC("Animation", RpcTarget.AllBuffered); 
        }
    }

    [PunRPC]
    public void Animation()
    {
        fenceAnim.SetTrigger("Lefty");
        wallAnim.CrossFade("WallFall", .1f);
    }
}