using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelController : MonoBehaviour
{
    public GameObject spellWheelBackground;
    public GameObject edgeObject;
    public GameObject wheelHiglighter;
    public WheelSelectorController wheelSelector;
    public GameObject wheelSlots;
    public GameObject wheelSlotImagePrefab;
    float slotSize = 0;
    WheelHiglighterContoller highlighterController;
    private SoundEmitter _soundEmitter;
    private FMOD.Studio.PARAMETER_ID _wheelParameterId;

    [SerializeField] private ManaBarIconController manaBarIconController;

    private ItemObject[] _itemList = new ItemObject[6];

    private Canvas _canvas;

    private bool _isClosed = false;

    void Awake()
    {
        _soundEmitter = GetComponent<SoundEmitter>();
        _canvas = GetComponent<Canvas>();
    }

    void Start()
    {
        slotSize = edgeObject.transform.position.x - spellWheelBackground.transform.position.x;
        highlighterController = wheelHiglighter.GetComponent<WheelHiglighterContoller>();
        _wheelParameterId = _soundEmitter.GetParameterId("wheel", "Abilities Menu Type");
        wheelSelector.enabled = false;
        highlighterController.enabled = false;
    }

    void Update()
    {
        if (_isClosed)
        {
            return;
        }
        Vector2 mouseVec = Input.mousePosition - spellWheelBackground.transform.position;
        if (mouseVec.magnitude < slotSize)
        {
            float angle = Vector2.Angle(mouseVec, transform.right);
            Debug.DrawRay(spellWheelBackground.transform.position, mouseVec, Color.yellow);
            if (angle < 30 && angle > -30)
            {
                SetHighlight(0);
                checkSlotSelection(2);
            }
            if (angle < 90 && angle > 30)
            {
                if (mouseVec.y > 0)
                {
                    SetHighlight(60);
                    checkSlotSelection(1);
                }
                else
                {
                    SetHighlight(-60);
                    checkSlotSelection(3);
                }
            }
            if (angle < 150 && angle > 90)
            {
                if (mouseVec.y > 0)
                {
                    SetHighlight(120);
                    checkSlotSelection(0);
                }
                else
                {
                    SetHighlight(-120);
                    checkSlotSelection(4);
                }
            }
            if (angle < -150 || angle > 150)
            {
                SetHighlight(180);
                checkSlotSelection(5);
            }
        }
    }

    private void SetHighlight(float angle)
    {
        if (highlighterController.TargetAngle == angle || !highlighterController.enabled)
        {
            return;
        }
        highlighterController.TargetAngle = angle;
        _soundEmitter.SetParameterWithLabel("wheel", _wheelParameterId, "Hover", true);
    }

    void checkSlotSelection(int slot)
    {
        if (Input.GetMouseButtonDown(0))
        {
            wheelSelector.changeSelection(slot);
            Debug.Log(_itemList[slot].Icon);
            if (_itemList[slot] == null)
            {
                manaBarIconController.changeSpellSprite(null);
            }
            else
            {
                manaBarIconController.changeSpellSprite(_itemList[slot].Icon);
            }
            //uiController.SelectSlotLeft(slot);
        }
    }

    public ItemSkill GetSelectedSlot()
    {
        ItemObject item = _itemList[wheelSelector.CurrentSlot];
        if (item == null)
        {
            return null;
        }
        return (ItemSkill)_itemList[wheelSelector.CurrentSlot];
    }

    public void updateSpellWheel(ItemObject[] itemList)
    {
        _itemList = itemList;
        for (int i = 0; i < itemList.Length; i++)
        {
            if (itemList[i] != null)
            {
                GameObject itemImage = Instantiate(wheelSlotImagePrefab);
                itemImage.transform.SetParent(wheelSlots.transform.GetChild(i));
                itemImage.transform.position = wheelSlots.transform.GetChild(i).position;
                itemImage.transform.localScale = new Vector2(1, 1);
                itemImage.GetComponent<Image>().sprite = itemList[i].Icon;
            }
            else
            {
                for (int j = 0; j < wheelSlots.transform.GetChild(i).childCount; j++)
                {
                    Destroy(wheelSlots.transform.GetChild(i).GetChild(j).gameObject);
                }
            }
        }
    }

    public void Open()
    {
        _soundEmitter.SetParameterWithLabel("wheel", _wheelParameterId, "Open", true);
        _canvas.enabled = true;
        _isClosed = false;
        wheelSelector.enabled = true;
        highlighterController.enabled = true;
    }

    public void Close()
    {
        _soundEmitter.SetParameterWithLabel("wheel", _wheelParameterId, "Close", true);
        _canvas.enabled = false;
        _isClosed = true;
        wheelSelector.enabled = false;
        highlighterController.enabled = false;
    }
}
