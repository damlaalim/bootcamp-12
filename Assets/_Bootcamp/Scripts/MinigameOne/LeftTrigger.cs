using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class LeftTrigger : MonoBehaviourPun
{
    public Animator fenceAnim;
    public Animator wallAnim;
    // Start is called before the first frame update
    [PunRPC]
    public void Animation1(Collider other)
    {
            fenceAnim.SetTrigger("Righty");
            wallAnim.SetTrigger("Wall Animation Begin");
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            photonView.RPC("Animation1", RpcTarget.AllBuffered,other);
        }
    
    }
    
   
}
