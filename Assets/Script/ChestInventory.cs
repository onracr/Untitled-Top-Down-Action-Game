using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChestInventory : Inventory, IOnClickEvents
{

    [SerializeField] private GameObject chestInventoryMenu = null;

    // References
    private Chests _chest;
    
    public override void DisplayInventory(List<Item> newItem)
    {
        OpenMenu();
        ItemArray = new Item[newItem.Count];
        for (var i = 0; i < ItemArray.Length; i++)
        {
            icon[i].GetComponent<Image>().enabled = true;
            icon[i].GetComponent<Image>().sprite = newItem[i].icon;
            ItemArray[i] = newItem[i];
        }
    }

    public void OpenMenu()
    {
        chestInventoryMenu.SetActive(true);
    }

    public void CloseMenu()
    {
        foreach (var t in icon)
        {
            t.GetComponent<Image>().enabled = false;
        }
        chestInventoryMenu.SetActive(false);
    }

    public void Test()
    {
        for (var i = 0; i < inventorySlot.Length; i++)
        {
            if (inventorySlot[i].name == EventSystem.current.currentSelectedGameObject.name)
            {
                PersonalInventory.Instance.AddToInventory(ItemArray[i]);
                icon[i].GetComponent<Image>().enabled = false;
                _chest.GetComponent<Chests>().UpdateChest(ItemArray[i]);
            }
        }
    }

    public void SetChestReference(Chests chestRef)
    {
        _chest = chestRef;
    }
}
