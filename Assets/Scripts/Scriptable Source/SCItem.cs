using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCItem : ScriptableObject
{
    public string itemName;
    public string description;
    public bool canStackable;
    public Sprite itemIcon;
    public GameObject itemPrefab;
}
