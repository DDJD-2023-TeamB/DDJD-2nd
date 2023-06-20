using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    private ItemsInventoryObject _itemsInventory;

    private PlayerUI _playerUI;

    private TutorialUI _tutorialUI;
    private Player _player;

    ItemSkill[] leftWheelItems = new ItemSkill[6];
    ItemSkill[] rightWheelItems = new ItemSkill[6];

    // Remover quando se fizer a função de remove
    ItemStack[] itemList = new ItemStack[30];

    public string currentMenu = "playing";

    // KEYS

    public KeyCode inventoryKey = KeyCode.Tab;
    public KeyCode menuKey = KeyCode.Escape;
    public KeyCode leftWheelKey = KeyCode.Q;
    public KeyCode rightWheelKey = KeyCode.E;
    public KeyCode missionsKey = KeyCode.M;
    public KeyCode activeElementKey = KeyCode.LeftAlt;

    public Sprite fireStoneSprite;
    public Sprite redDiamondSprite;

    public GameObject currentItemTitle;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _playerUI = GameObject.FindGameObjectWithTag("PlayerUI").GetComponent<PlayerUI>();
    }

    private void Start()
    {
        _playerUI = GameObject.FindGameObjectWithTag("PlayerUI").GetComponent<PlayerUI>();
        _playerUI.inventoryUI.gameObject.SetActive(false);
        _playerUI.menuUI.SetActive(false);
        _playerUI.missionsUI.SetActive(false);
        _playerUI.activeElementWheel.SetActive(false);
        _playerUI.OptionsUI.SetUIController(this);

        InventoryUI inventoryUI = _playerUI.inventoryUI;
        inventoryUI.OnItemSkillLeftDrop += ChangeLeftWheelItem;
        inventoryUI.OnItemSkillRightDrop += ChangeRightWheelItem;
        inventoryUI.OnItemSkillDrop += DropItemSkill;
        inventoryUI.SetupActions();

        _itemsInventory = _player.Inventory;

        UpdateSpellWheels();
        // Items and spells
        LoadInventory();
    }

    public void OpenInventory(bool isOpening)
    {
        currentMenu = "inventory";
        _playerUI.inventoryUI.gameObject.SetActive(isOpening);
        _playerUI.playingUI.gameObject.SetActive(!isOpening);

        if (isOpening)
        {
            _playerUI.inventoryUI.RemoveAllItems();
            LoadItems();
        }
    }

    public void OpenMenu(bool isOpening)
    {
        currentMenu = "menu";
        _playerUI.menuUI.SetActive(isOpening);
        _playerUI.playingUI.gameObject.SetActive(!isOpening);
    }


    public void OpenTutorial(bool isOpening)
    {
        currentMenu = "tutorial";
        _playerUI.tutorialUI.SetActive(isOpening);
        _playerUI.playingUI.gameObject.SetActive(!isOpening);
        
        if (isOpening == true) _tutorialUI = _playerUI.tutorialUI.GetComponent<TutorialUI>();
        else _tutorialUI = null;
    }

    public void HandleTutorial(bool isOpening, Tutorial tutorial)
    {
        OpenTutorial(isOpening);
        ChangeTutorialPage(tutorial);
    }

    public void ChangeTutorialPage(Tutorial tutorial)
    {
        _tutorialUI.ShowUI(tutorial);
    }

    public void OpenOptions(bool isOpening)
    {
        currentMenu = "options";
        _playerUI.OptionsUI.gameObject.SetActive(isOpening);
        _playerUI.menuUI.SetActive(!isOpening);
    }

    public void OpenMissions(bool isOpening)
    {
        currentMenu = "missions";
        _playerUI.missionsUI.SetActive(isOpening);
        _playerUI.playingUI.gameObject.SetActive(!isOpening);
    }

    public void OpenLeftSpell(bool isOpening)
    {
        if (isOpening)
        {
            _playerUI.leftSpellWheel.Open();
        }
        else
        {
            _playerUI.leftSpellWheel.Close();
        }
    }

    public void OpenRightSpell(bool isOpening)
    {
        if (isOpening)
        {
            _playerUI.rightSpellWheel.Open();
        }
        else
        {
            _playerUI.rightSpellWheel.Close();
        }
    }

    public void OpenActiveElement(bool isOpening)
    {
        if (_playerUI.activeElementWheel.activeInHierarchy != isOpening)
        {
            _playerUI.activeElementWheel.SetActive(isOpening);
        }
    }

    public AimedSkill GetLeftSkillSelected()
    {
        ItemSkill itemSkill = _playerUI.leftSpellWheel.GetSelectedSlot();
        if (itemSkill == null)
        {
            return null;
        }
        return itemSkill.Skill;
    }

    public AimedSkill GetRightSkillSelected()
    {
        ItemSkill itemSkill = _playerUI.rightSpellWheel.GetSelectedSlot();
        if (itemSkill == null)
        {
            return null;
        }
        return itemSkill.Skill;
    }

    private void Update() { }

    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        _playerUI.playingUI.UpdateHealth(currentHealth, maxHealth);
    }

    public void UpdateMana(Element element, int currentMana, int maxMana)
    {
        _playerUI.playingUI.UpdateMana(element, currentMana, maxMana);
    }

    public void UpdateElements(Skill leftSkill, Skill rightSkill, Element element)
    {
        _playerUI.playingUI.UpdateElements(leftSkill, rightSkill, element);
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
        leftWheelItems = _player.PlayerSkills.EquippedLeftSkills.ToArray();
        rightWheelItems = _player.PlayerSkills.EquippedRightSkills.ToArray();
        _playerUI.leftSpellWheel
            .GetComponent<WheelController>()
            .updateSpellWheel(leftWheelItems, UiArea.LeftWheel);
        _playerUI.rightSpellWheel
            .GetComponent<WheelController>()
            .updateSpellWheel(rightWheelItems, UiArea.RightWheel);
        _playerUI.inventoryUI.SetLeftWheelSkills(new List<ItemSkill>(leftWheelItems));
        _playerUI.inventoryUI.SetRightWheelSkills(new List<ItemSkill>(rightWheelItems));
    }

    public void AddItem(ItemStack item, UiArea area)
    {
        _playerUI.inventoryUI.AddItem(item, area);
    }

    private void RemoveFromWheel(ItemStack itemStack, bool isLeft)
    {
        //Find item in wheel
        ItemSkill[] wheelItems = isLeft ? leftWheelItems : rightWheelItems;
        List<ItemSkill> equippedSkills = isLeft
            ? _player.PlayerSkills.EquippedLeftSkills
            : _player.PlayerSkills.EquippedRightSkills;
        for (int i = 0; i < wheelItems.Length; i++)
        {
            if (wheelItems[i] == null)
            {
                equippedSkills[i] = null;
                continue;
            }
            if (wheelItems[i] == itemStack.item)
            {
                equippedSkills[i] = null;
                wheelItems[i] = null;
                break;
            }
        }
        UpdateSpellWheels();
    }

    public void DropItemSkill(InventoryItemImage image, int slot, UiArea area)
    {
        if (!(image.currentItem.item is ItemSkill))
        {
            Debug.LogError("Item is not itemskill");
            return;
        }
        if (area != UiArea.Spells)
        {
            // Remove item from it's previous position
            if (area == UiArea.LeftWheel)
            {
                RemoveFromWheel(image.currentItem, true);
                Destroy(image.gameObject);
            }
            else if (area == UiArea.RightWheel)
            {
                RemoveFromWheel(image.currentItem, true);
                Destroy(image.gameObject);
            }
        }
    }

    public void ChangeLeftWheelItem(InventoryItemImage image, int slot, UiArea area)
    {
        if (!(image.currentItem.item is ItemSkill))
        {
            Debug.LogError("Item is not itemskill");
            return;
        }
        ItemSkill itemSkill = (ItemSkill)image.currentItem.item;
        leftWheelItems[slot] = itemSkill;
        _player.PlayerSkills.EquippedLeftSkills[slot] = itemSkill;
        UpdateSpellWheels();
        _playerUI.inventoryUI.SetLeftWheelSkills(new List<ItemSkill>(leftWheelItems));
    }

    public void ChangeRightWheelItem(InventoryItemImage image, int slot, UiArea area)
    {
        if (!(image.currentItem.item is ItemSkill))
        {
            Debug.LogError("Item is not itemskill");
            return;
        }
        ItemSkill itemSkill = (ItemSkill)image.currentItem.item;
        rightWheelItems[slot] = itemSkill;
        _player.PlayerSkills.EquippedRightSkills[slot] = itemSkill;
        UpdateSpellWheels();
        _playerUI.inventoryUI.SetRightWheelSkills(new List<ItemSkill>(rightWheelItems));
    }

    public void LoadSpells()
    {
        foreach (ItemSkill itemSkill in _player.PlayerSkills.LearnedSkills)
        {
            AddItem(new ItemStack(itemSkill, 1, null), UiArea.Spells);
        }
    }

    public void LoadItems()
    {
        foreach (var item in _itemsInventory.Container)
        {
            AddItem(item, UiArea.Items);
        }
    }

    public void LoadInventory()
    {
        LoadSpells();
        _playerUI.inventoryUI.SetLeftWheelSkills(new List<ItemSkill>(leftWheelItems));
        _playerUI.inventoryUI.SetRightWheelSkills(new List<ItemSkill>(rightWheelItems));
        LoadItems();
    }

    public Player Player
    {
        get { return _player; }
    }

    public PlayerUI PlayerUI
    {
        get { return _playerUI; }
    }

    public void showCompleteMissionText(string missionTitle)
    {
        _playerUI.playingUI.missionCompleteNotification.StartAnimation(missionTitle);
    }
}
