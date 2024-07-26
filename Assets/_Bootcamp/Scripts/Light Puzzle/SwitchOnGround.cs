using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchOnGround : MonoBehaviour
{
    public static SwitchOnGround Instance;
    public GameObject switchOnWall;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlaceSwitch()
    {
        switchOnWall.SetActive(true);
    }
    public void DestroySwitch()
    {
        this.gameObject.SetActive(false);
    }

}
