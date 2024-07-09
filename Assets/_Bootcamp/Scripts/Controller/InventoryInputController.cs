using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryInputController : MonoBehaviour
{
    [System.Serializable]
    public class InventoryItem
    {
        public GameObject inventoryObject;
        public Button inventoryButton;
        public KeyCode key;
        [HideInInspector]
        public bool isButtonPressed = false;
    }

    public List<InventoryItem> inventoryItems = new List<InventoryItem>();

    void Update()
    {
        foreach (InventoryItem item in inventoryItems)
        {
            if (Input.GetKeyDown(item.key))
            {
                ToggleObject(item);
            }
        }
    }

    void ToggleObject(InventoryItem item)
    {
        if (item.inventoryObject != null)
        {
            item.inventoryObject.SetActive(!item.inventoryObject.activeSelf);
        }

        if (item.inventoryButton != null)
        {
            if (!item.isButtonPressed)
            {
                ExecuteEvents.Execute(item.inventoryButton.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);
            }
            else
            {
                ExecuteEvents.Execute(item.inventoryButton.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerUpHandler);
            }

            item.isButtonPressed = !item.isButtonPressed;
        }
    }
}
