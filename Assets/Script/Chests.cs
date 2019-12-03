using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Chests : MonoBehaviour, ICollectables
{
    public Item[] items;
    private List<Item> _itemList;
    
    public int pesosAmount;
    private bool _triggered = false;
    
    public Sprite sprite, emptyChestSprite;
    private Sprite _defaultSprite;
    private SpriteRenderer _spriteRenderer;
    
    // References
    public ChestInventory chestInventory;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultSprite = _spriteRenderer.sprite;
        _itemList = new List<Item>();
        
        for (var i = 0; i < items.Length; i++)
        {
            _itemList.Add(items[i]);
        }
    }

    private void Update()
    {
        if (_triggered) OnCollect();
    }

    public void OnCollect()
    {
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            chestInventory.SetChestReference(this);
            chestInventory.DisplayInventory(_itemList);
            //GameManager.Instance.pesos += pesosAmount; 
            //GameManager.Instance.ShowText(
              //  "+" + pesosAmount + "pesos!", 21, Color.yellow,
                //transform.position, Vector3.up * 25, 1.0f); 
        }
    }

    public void UpdateChest(Item item)
    {
        _itemList.Remove(item);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        _spriteRenderer.sprite = sprite;
        _triggered = true;
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        _spriteRenderer.sprite = _defaultSprite;
        _triggered = false;
    }
}
