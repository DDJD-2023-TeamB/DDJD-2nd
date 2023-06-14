using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

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
        TextMeshProUGUI text;
        GameObject myText;

        RectTransform rectTransform;
        FloatingText floatingText;

        ColorUtility.TryParseHtmlString("#FED600", out highlightColor);

        // Text
        myText = new GameObject();
        myText.transform.parent = myGO.transform;
        myText.name = "itemName";

        text = myText.AddComponent<TextMeshProUGUI>();
        text.text = name;
        text.fontSize = 15;
        text.color = highlightColor;
        text.alignment = TextAlignmentOptions.Center;
        // Text position
        rectTransform = text.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(0, 0, 0);
        rectTransform.sizeDelta = new Vector2(200, 50);

        floatingText = myText.AddComponent<FloatingText>();
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
