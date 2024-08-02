using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class RightTrigger : MonoBehaviourPun
{
    public Animator fenceAnim;
    public Animator wallAnim;
   
    [PunRPC]
    public void Animation2(Collider other)
    {
            fenceAnim.SetTrigger("Righty");
            wallAnim.SetTrigger("Wall Animation Begin");
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        { 
            photonView.RPC("Animation2", RpcTarget.AllBuffered,other);
        }

       
    }
}
