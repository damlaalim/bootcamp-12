using System;
using Photon.Pun;
using UnityEngine;

namespace _Bootcamp.PhotonBurak.Scripts
{
    public class LightInteraction : MonoBehaviourPun
    {
        private bool isNearCube;

        private void Update()
        {
            if(isNearCube && Input.GetKeyDown(KeyCode.E))
            {
                GameObject lightObject = GameObject.Find("LightObjectName"); 
                if (lightObject != null)
                {
                    PhotonView photonView = lightObject.GetComponent<PhotonView>();
                    if (photonView != null)
                    {
                        Debug.Log("PhotonView bulundu ve RPC çağrısı yapılacak");
                        photonView.RPC("ChangeLightColor", RpcTarget.All);
                    }
                    else
                    {
                        Debug.LogError("PhotonView bulunamadı");
                    }
                }
                else
                {
                    Debug.LogError("LightObjectName adında nesne bulunamadı");
                }
            }
        }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cube"))
        {
            isNearCube = true;
            Debug.Log("triggerlandı");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Cube"))
        {
            isNearCube = false;
        }
    }
     
    }
}