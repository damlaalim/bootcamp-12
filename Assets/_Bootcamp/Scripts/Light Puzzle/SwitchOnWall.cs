using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchOnWall : MonoBehaviour
{
    public static SwitchOnWall Instance;
    public Animator platformAnim;
    private void Awake()
    {
        Instance = this;
    }
    public void WallSwitch()
    {
        platformAnim.SetTrigger("SwitchPulled");
    }
}
