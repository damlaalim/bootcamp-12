using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create new Item")]
public class Item : ScriptableObject
{
    public int id;
    public string itemName, info;
    public int value;
    public Sprite icon;
}
