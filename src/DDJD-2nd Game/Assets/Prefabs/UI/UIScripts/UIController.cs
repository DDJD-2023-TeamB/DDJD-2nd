using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private PlayerUI _playerUI;

    ItemStack[] leftWheelItems = new ItemStack[6];
    ItemStack[] rightWheelItems = new ItemStack[6];
    ItemStack[] itemList = new ItemStack[30];

    public string currentMenu = "playing";

    //KEYS

    public KeyCode inventoryKey = KeyCode.Tab;
    public KeyCode menuKey = KeyCode.Escape;
    public KeyCode leftWheelKey = KeyCode.Q;
    public KeyCode rightWheelKey = KeyCode.E;
    public KeyCode missionsKey = KeyCode.M;

    public Sprite fireStoneSprite;
    public Sprite redDiamondSprite;

    private void Start()
    {
        _playerUI = GameObject.FindGameObjectWithTag("PlayerUI").GetComponent<PlayerUI>();

        _playerUI.inventoryUI.SetActive(false);
        _playerUI.menuUI.SetActive(false);
        _playerUI.leftSpellWheel.SetActive(false);
        _playerUI.rightSpellWheel.SetActive(false);
        _playerUI.missionsUI.SetActive(false);

        //DEBUG
        LoadInventory();
    }

    public void OpenInventory(bool isOpening)
    {
        currentMenu = "inventory";
        _playerUI.inventoryUI.SetActive(isOpening);
        _playerUI.playingUI.SetActive(!isOpening);
    }

    public void OpenMenu(bool isOpening)
    {
        currentMenu = "menu";
        _playerUI.menuUI.SetActive(isOpening);
        _playerUI.playingUI.SetActive(!isOpening);
    }

    public void OpenMissions(bool isOpening)
    {
        currentMenu = "missions";
        _playerUI.missionsUI.SetActive(isOpening);
        _playerUI.playingUI.SetActive(!isOpening);
    }

    public void OpenLeftSpell(bool isOpening)
    {
        if (_playerUI.leftSpellWheel.activeInHierarchy != isOpening)
        {
            _playerUI.leftSpellWheel.SetActive(isOpening);
        }
    }

    public void OpenRightSpell(bool isOpening)
    {
        if (_playerUI.rightSpellWheel.activeInHierarchy != isOpening)
        {
            _playerUI.rightSpellWheel.SetActive(isOpening);
        }
    }

    private void Update()
    {
        return;
        if (currentMenu == "playing" && Input.GetKeyDown(inventoryKey))
        {
            currentMenu = "inventory";
            _playerUI.inventoryUI.SetActive(true);
            _playerUI.playingUI.SetActive(false);
            _playerUI.leftSpellWheel.SetActive(false);
            _playerUI.rightSpellWheel.SetActive(false);
        }
        else if (currentMenu == "playing" && Input.GetKeyDown(menuKey))
        {
            currentMenu = "menu";
            _playerUI.menuUI.SetActive(true);
            _playerUI.playingUI.SetActive(false);
            _playerUI.leftSpellWheel.SetActive(false);
            _playerUI.rightSpellWheel.SetActive(false);
        }
        else if (currentMenu == "playing" && Input.GetKeyDown(missionsKey))
        {
            currentMenu = "missions";
            _playerUI.missionsUI.SetActive(true);
            _playerUI.playingUI.SetActive(false);
            _playerUI.leftSpellWheel.SetActive(false);
            _playerUI.rightSpellWheel.SetActive(false);
        }
        else if (currentMenu == "playing" && Input.GetKeyDown(leftWheelKey))
        {
            _playerUI.leftSpellWheel.SetActive(true);
        }
        else if (currentMenu == "playing" && Input.GetKeyDown(rightWheelKey))
        {
            _playerUI.rightSpellWheel.SetActive(true);
        }
        else if (Input.GetKeyUp(leftWheelKey))
        {
            _playerUI.leftSpellWheel.SetActive(false);
        }
        else if (Input.GetKeyUp(rightWheelKey))
        {
            _playerUI.rightSpellWheel.SetActive(false);
        }
        else if (currentMenu == "inventory" && Input.GetKeyDown(inventoryKey))
        {
            currentMenu = "playing";
            _playerUI.inventoryUI.SetActive(false);
            _playerUI.playingUI.SetActive(true);
        }
        else if (currentMenu == "missions" && Input.GetKeyDown(missionsKey))
        {
            currentMenu = "playing";
            _playerUI.missionsUI.SetActive(false);
            _playerUI.playingUI.SetActive(true);
        }
        else if (currentMenu == "menu" && Input.GetKeyDown(menuKey))
        {
            currentMenu = "playing";
            _playerUI.menuUI.SetActive(false);
            _playerUI.playingUI.SetActive(true);
        }
    }

    public void SelectSlotLeft(int slot)
    {
        //Select spell on game controller
    }

    public void SelectSlotRight(int slot)
    {
        //Select spell on game controller
    }

    public void UpdateSpellWheels()
    {
        _playerUI.leftSpellWheel.GetComponent<WheelController>().updateSpellWheel(leftWheelItems);
        _playerUI.rightSpellWheel.GetComponent<WheelController>().updateSpellWheel(rightWheelItems);
    }

    public Boolean AddItem(ItemStack item)
    {
        for (int i = 0; i < itemList.Length; i++)
        {
            if (itemList[i] == null)
            {
                itemList[i] = item;
                _playerUI.inventoryUI.GetComponent<InventoryUI>().AddItem(item);
                return true;
            }
        }
        return false;
    }

    public ItemStack RemoveItem(ItemStack item)
    {
        for (int i = 0; i < itemList.Length; i++)
        {
            if (itemList[i] == item)
            {
                ItemStack removedItem = itemList[i];
                itemList[i] = null;
                _playerUI.inventoryUI.GetComponent<InventoryUI>().RemoveItem(removedItem);
                return removedItem;
            }
        }
        return null;
    }

    public ItemStack RemoveItem(string itemID)
    {
        for (int i = 0; i < itemList.Length; i++)
        {
            if (itemList[i].id == itemID)
            {
                ItemStack removedItem = itemList[i];
                itemList[i] = null;
                _playerUI.inventoryUI.GetComponent<InventoryUI>().RemoveItem(removedItem);
                return removedItem;
            }
        }
        return null;
    }

    public void ChangeLeftWheelItem(int slot, ItemStack item)
    {
        leftWheelItems[slot] = item;
        _playerUI.leftSpellWheel.GetComponent<WheelController>().updateSpellWheel(leftWheelItems);
    }

    public void ChangeRightWheelItem(int slot, ItemStack item)
    {
        rightWheelItems[slot] = item;
        _playerUI.rightSpellWheel.GetComponent<WheelController>().updateSpellWheel(rightWheelItems);
    }

    public void LoadInventory()
    {
        //Should load items from a game controller, this is just test code
        ItemType firestoneItem = new ItemType(
            "firestone",
            "Fire Stone",
            10,
            fireStoneSprite,
            false,
            false
        );
        ItemType redDiamondItem = new ItemType(
            "redDiamond",
            "Red Diamond",
            10,
            redDiamondSprite,
            true,
            true
        );
        ItemStack firestoneStack1 = new ItemStack(firestoneItem, null);
        ItemStack firestoneStack2 = new ItemStack(firestoneItem, null);
        ItemStack firestoneStack3 = new ItemStack(firestoneItem, null);
        ItemStack firestoneStack4 = new ItemStack(firestoneItem, null);
        ItemStack firestoneStack5 = new ItemStack(firestoneItem, null);
        ItemStack redDiamondStack1 = new ItemStack(redDiamondItem, null);
        ItemStack redDiamondStack2 = new ItemStack(redDiamondItem, null);
        redDiamondStack2.amount = 427;
        AddItem(firestoneStack1);
        AddItem(firestoneStack2);
        AddItem(firestoneStack3);
        AddItem(firestoneStack4);
        AddItem(firestoneStack5);
        AddItem(redDiamondStack1);
        AddItem(redDiamondStack2);
    }
}

public class ItemStack
{
    public ItemType type;
    public int amount;
    public string id;

    public ItemStack(ItemType itemType, string id)
    {
        this.type = itemType;
        this.amount = 1;
        if (id != null)
        {
            this.id = id;
        }
        else
        {
            id = GenerateRandomStringHash(10);
        }
    }

    static System.Random random = new System.Random();

    string GenerateRandomStringHash(int hashLength)
    {
        string Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < hashLength; i++)
        {
            int randomIndex = random.Next(Characters.Length);
            stringBuilder.Append(Characters[randomIndex]);
        }

        return stringBuilder.ToString();
    }
}

public class ItemType
{
    public string typeID;
    public int maxItems;
    public bool isConsumable;
    public bool isSpell;
    public Sprite itemSprite;
    public string name;

    public ItemType(
        string typeID,
        string name,
        int maxItems,
        Sprite itemSprite,
        bool isConsumable,
        bool isSpell
    )
    {
        this.typeID = typeID;
        this.maxItems = maxItems;
        this.itemSprite = itemSprite;
        this.isConsumable = isConsumable;
        this.isSpell = isSpell;
        this.name = name;
    }
}
