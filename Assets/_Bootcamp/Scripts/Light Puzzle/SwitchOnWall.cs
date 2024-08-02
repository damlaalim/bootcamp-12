using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SwitchOnWall : MonoBehaviourPunCallbacks
{
    public static SwitchOnWall Instance;
    public Animator platformAnim;
    public Animator switchAnim;
    private void Awake()
    {
        Instance = this;
    }
    public void WallSwitch()
    {
        photonView.RPC("WallSwitchRPC", RpcTarget.AllBuffered);
    }

    [PunRPC]
   private  void WallSwitchRPC()
    {
        platformAnim.SetTrigger("SwitchPulled");
        switchAnim.SetTrigger("switchAnim");
    }
}
