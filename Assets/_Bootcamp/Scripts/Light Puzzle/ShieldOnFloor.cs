using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldOnFloor : MonoBehaviour
{
    public static ShieldOnFloor Instance;
    public GameObject lightPointOnWall;
    private void Awake()
    {
        Instance = this;
    }
    public void PlaceLightPoint()
    {
        lightPointOnWall.SetActive(true);
    }
    public void DestroyLightPoint()
    {
        this.gameObject.SetActive(false);
    }
}
