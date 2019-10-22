using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;

public abstract class Inventory : MonoBehaviour
{
    public GameObject[] inventorySlot, icon = null;

    protected Item[] ItemArray;

    protected virtual void Update()
    {}
    
    public virtual void DisplayInventory(List<Item> newItem)
    {
    }

    public virtual void AddToInventory(Item item)
    {}
    
}
