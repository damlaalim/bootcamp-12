using System;
using System.Collections.Generic;
using _Bootcamp.Scripts.Interactable;
using ModestTree;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public CollectableController[] Items;

    public Transform ItemContent;
    public GameObject InventoryItem;

    [SerializeField] private int _itemCapacity;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        // başta belirlenen kapasitede bir array oluşturuyor
        Items = new CollectableController[_itemCapacity];
    }

    public void Add(CollectableController item)
    {
        // items içerisinde yer varsa ekliyor 

        for (var i = 0; i < Items.Length; i++)
        {
            if (Items[i] != null) continue;
            
            Items[i] = item;
            // item.GameObject().transform.do
            break;
        }
    }

    public void Remove(CollectableController item)
    {
        var index = Items.IndexOf(item);
        Items[index] =  null;
    }

    public void ListItems()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<Text>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

            itemName.text = item.data.itemName;
            itemIcon.sprite = item.data.icon;
        }
    }
}
