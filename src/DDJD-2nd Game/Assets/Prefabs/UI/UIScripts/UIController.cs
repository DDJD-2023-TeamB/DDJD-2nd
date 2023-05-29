using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private PlayerUI _playerUI;
    private Player _player;

    ItemSkill[] leftWheelItems = new ItemSkill[6];
    ItemSkill[] rightWheelItems = new ItemSkill[6];
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

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void Start()
    {
        _playerUI = GameObject.FindGameObjectWithTag("PlayerUI").GetComponent<PlayerUI>();

        _playerUI.inventoryUI.gameObject.SetActive(false);
        _playerUI.menuUI.SetActive(false);
        _playerUI.leftSpellWheel.SetActive(false);
        _playerUI.rightSpellWheel.SetActive(false);
        _playerUI.missionsUI.SetActive(false);

        InventoryUI inventoryUI = _playerUI.inventoryUI;
        //inventoryUI.OnItemDrop += ;
        //inventoryUI.OnItemSkillDrop += ;
        inventoryUI.OnItemSkillLeftDrop += ChangeLeftWheelItem;
        inventoryUI.OnItemSkillRightDrop += ChangeRightWheelItem;
        inventoryUI.SetupActions();

        //DEBUG


        UpdateSpellWheels();
        LoadInventory();
    }

    public void OpenInventory(bool isOpening)
    {
        currentMenu = "inventory";
        _playerUI.inventoryUI.gameObject.SetActive(isOpening);
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

    public AimedSkill GetLeftSkillSelected()
    {
        ItemSkill itemSkill = _playerUI.leftSpellWheel
            .GetComponent<WheelController>()
            .GetSelectedSlot();
        return itemSkill.Skill;
    }

    public AimedSkill GetRightSkillSelected()
    {
        ItemSkill itemSkill = _playerUI.rightSpellWheel
            .GetComponent<WheelController>()
            .GetSelectedSlot();
        return itemSkill.Skill;
    }

    private void Update() { }

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
        leftWheelItems = _player.PlayerSkills.EquippedLeftSkills.ToArray();
        rightWheelItems = _player.PlayerSkills.EquippedRightSkills.ToArray();
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
                _playerUI.inventoryUI.AddItem(item);
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
                _playerUI.inventoryUI.RemoveItem(removedItem);
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
                _playerUI.inventoryUI.RemoveItem(removedItem);
                return removedItem;
            }
        }
        return null;
    }

    public void ChangeLeftWheelItem(ItemStack itemStack, int slot)
    {
        if (!(itemStack.type is ItemSkill))
        {
            Debug.Log("Is not itemskill");
            return;
        }
        ItemSkill itemSkill = (ItemSkill)itemStack.type;
        leftWheelItems[slot] = itemSkill;
        _player.PlayerSkills.EquippedLeftSkills[slot] = itemSkill;
        _playerUI.leftSpellWheel.GetComponent<WheelController>().updateSpellWheel(leftWheelItems);
        UpdateSpellWheels();
        _playerUI.inventoryUI.SetLeftWheelSkills(new List<ItemSkill>(leftWheelItems));
    }

    public void ChangeRightWheelItem(ItemStack itemStack, int slot)
    {
        if (!(itemStack.type is ItemSkill))
        {
            Debug.Log("Is not itemskill");
            return;
        }
        ItemSkill itemSkill = (ItemSkill)itemStack.type;
        rightWheelItems[slot] = itemSkill;
        _player.PlayerSkills.EquippedRightSkills[slot] = itemSkill;
        _playerUI.rightSpellWheel.GetComponent<WheelController>().updateSpellWheel(rightWheelItems);
        UpdateSpellWheels();
        _playerUI.inventoryUI.SetRightWheelSkills(new List<ItemSkill>(rightWheelItems));
    }

    public void LoadSpells()
    {
        foreach (ItemSkill itemSkill in _player.PlayerSkills.LearnedSkills)
        {
            AddItem(new ItemStack(itemSkill, null));
        }
    }

    public void LoadInventory()
    {
        LoadSpells();
        _playerUI.inventoryUI.SetLeftWheelSkills(new List<ItemSkill>(leftWheelItems));
        _playerUI.inventoryUI.SetRightWheelSkills(new List<ItemSkill>(rightWheelItems));

        /*
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
        */
    }

    public Player Player
    {
        get { return _player; }
    }
}

public class ItemStack
{
    public Item type;
    public int amount;
    public string id;

    public ItemStack(Item itemType, string id)
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

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        ItemStack otherItem = (ItemStack)obj;
        return type.Equals(otherItem.type);
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
