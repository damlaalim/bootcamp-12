using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deneme : MonoBehaviour
{
    public GameObject myCanvas;


    void Update()
    {
        // 1 tuşuna basıldığında Canvas'ın aktiflik durumunu değiştir
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
                myCanvas.SetActive(!myCanvas.activeSelf); // Aktiflik durumunu tersine çevir
        }
    }
}