using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftTrigger : MonoBehaviour
{
    public Animator fenceAnim;
    public Animator wallAnim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            fenceAnim.SetTrigger("Lefty");
            wallAnim.SetTrigger("Wall Animation Begin");
        }
    }
}
