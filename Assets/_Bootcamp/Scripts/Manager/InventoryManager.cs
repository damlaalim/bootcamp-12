using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<GameObject> inventoryObjects = new List<GameObject>();
    public void UseObject(GameObject usedObject)
    {
        foreach (GameObject obj in inventoryObjects)
        {
            obj.SetActive(true);
        }
    }

    public void StopUsingObject(GameObject usedObject)
    {
        foreach (GameObject obj in inventoryObjects)
        {
            obj.SetActive(false);
        }
    }

    void Update()
    {
        //kullan
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            UseObject(null); 
        }
        //bırak
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StopUsingObject(null); 
        }
    }
}
