using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PersonalInventory : Inventory, IOnClickEvents
{
    [SerializeField] private GameObject playerInventory = null, inventoryOptionPrefab = null;
    [SerializeField] private GameObject inspectorMenu = null, backgroundExit = null;
    [SerializeField] private Image inspectorMenuItemImage = null;
    [SerializeField] private Text inspectorMenuItemTitleTxt = null;
    [SerializeField] private Text inspectorMenuItemInfoTxt = null;
    
    private List<Item> _inventoryItems = new List<Item>();
    private Item _tempItem = null;
    private int _usage;
    private Animator _animator;
    private GameObject _inventoryOption;

    #region Singleton

    public static PersonalInventory Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("There was more than one instance of Personal Inventory!");
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            OpenMenu();
    }

    public override void AddToInventory(Item item)
    {
        if (_inventoryItems.Find(x => x.icon == item.icon) && _usage != 0)
        {
            foreach (var i in icon)
            {
                if (i.GetComponent<Image>().sprite == item.icon && item.itemName != "Moolah")
                {
                    GameManager.Instance.itemCount++;
                    i.transform.GetChild(0).gameObject.SetActive(true);
                    i.transform.GetChild(0).GetChild(0).GetComponent<Text>().text =
                        GameManager.Instance.itemCount.ToString();
                }
            }
            Debug.Log("Same Item"); 
            return;
        }
        _inventoryItems.Add(item);
        
        icon[_usage].GetComponent<Image>().enabled = true;
        icon[_usage].GetComponent<Image>().sprite = item.icon;

        _usage++;

            Debug.Log(_inventoryItems.Count);
    }

    public void OpenMenu()
    {
        playerInventory.SetActive(true);
    }

    public void CloseMenu()
    {
        playerInventory.SetActive(false);
    }

    public void OpenInventoryOptionMenu()
    {
        //_animator.SetTrigger("Appeared");
        
        for (var i = 0; i < inventorySlot.Length; i++)
        {
            //if (_inventoryIcons[i] == null) return;
            if (inventorySlot[i].name == EventSystem.current.currentSelectedGameObject.name)
            {
                _inventoryOption = Instantiate(
                    inventoryOptionPrefab,
                    inventorySlot[i].transform.position + new Vector3(55f, -30f, 0),
                    Quaternion.identity,
                    this.transform
                    );
                backgroundExit.SetActive(true);

                foreach (var item in _inventoryItems)
                {
                    if (item.icon == icon[i].GetComponent<Image>().sprite)
                    {
                        _tempItem = item;
                        if (item.itemName != "weapon")
                        {
                            _inventoryOption.transform.GetChild(1).gameObject.SetActive(false);
                            _inventoryOption.transform.GetChild(2).gameObject.SetActive(true);
                        }
                        else
                        {
                            _inventoryOption.transform.GetChild(1).gameObject.SetActive(true);
                            _inventoryOption.transform.GetChild(2).gameObject.SetActive(false);
                        }
                            
                    }
                }
                _inventoryOption.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(OnClickEquipItem);    
                _inventoryOption.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(OnClickEquipItem);
                _inventoryOption.transform.GetChild(4).GetComponent<Button>().onClick.AddListener(OpenInspectorMenu);
            }
        }
    }

    public void CloseInventoryOptionMenu()
    {
        //_animator.SetTrigger("Hidden");
        Destroy(_inventoryOption);
        backgroundExit.SetActive(false);
    }

    private void OpenInspectorMenu()
    {
        inspectorMenuItemImage.sprite = _tempItem.icon;
        if (_tempItem is ItemWeapon itemWeapon)
        {
            if (itemWeapon != null) inspectorMenuItemTitleTxt.text = itemWeapon.weaponName;
        }
        else
        {
            inspectorMenuItemTitleTxt.text = _tempItem.itemName;
        }

        inspectorMenuItemInfoTxt.text = _tempItem.itemInfo;
        
        inspectorMenu.SetActive(true);
        backgroundExit.SetActive(false);
        Destroy(_inventoryOption);
    }
    
    public void CloseInspectorMenu()
    {
        inspectorMenu.SetActive(false);
    }

    private void OnClickEquipItem()
    {
        if (_tempItem is ItemWeapon)
            Weapon.Instance.EquipItem(_tempItem as ItemWeapon);
        
        Destroy(_inventoryOption);
        backgroundExit.SetActive(false);
    }

    

    public void DropItem()
    {
        
    }
    
}
