using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Animations;
using System.Linq;

public class CollectibleInteractable : Interactable
{
    public CollectibleObject _item;

    public TextMeshProUGUI textPrefab;

    public FloatingText floatingText;

    Color highlightColor;

    protected override void Start()
    {
        base.Start();
        if (_item.HasToPay)
        {
            CreateCanvas();
        }
    }

    public void CreateCanvas()
    {
        GameObject myGO;
        Canvas myCanvas;

        myGO = new GameObject();
        myGO.transform.parent = gameObject.transform;
        myGO.name = "Canvas";
        myGO.AddComponent<Canvas>();

        myCanvas = myGO.GetComponent<Canvas>();
        myCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        myGO.AddComponent<CanvasScaler>();
        myGO.AddComponent<GraphicRaycaster>();

        AddTextToCanvas(myGO, _item.Cost.ToString() + "$");
    }

    public void AddTextToCanvas(GameObject myGO, string name)
    {
        GameObject floatingPrefab = GameObject.Find("FloatingText");

        GameObject floatingText = Instantiate(floatingPrefab);
        TextMeshProUGUI text = floatingText.GetComponent<TextMeshProUGUI>();
        text.text = name;

        floatingText.transform.SetParent(myGO.transform, false);
    }

    protected override void Approach()
    {
        if (_item.HasToPay)
            HelpManager.Instance.SetHelpText("Press F to purchase");
        else
        {
            HelpManager.Instance.SetHelpText("Press F to collect");
        }
    }

    public override void Interact()
    {
        if (!_item.HasToPay)
        {
            _player.Inventory.AddItem(_item, 1);
            Destroy(gameObject);
            _missionController.CheckIfItemCollectedIsMyGoal(_item);
            HelpManager.Instance.ResetText();
        }
        else
        {
            if (_player.Inventory.SubGold(_item.Cost))
            {
                _player.Inventory.AddItem(_item, 1);
                Destroy(gameObject);
                HelpManager.Instance.ResetText();
            }
            else
            {
                _player.InteractedObject = null;
                HelpManager.Instance.SetHelpText("Not enough gold");
            }
        }
    }
}
