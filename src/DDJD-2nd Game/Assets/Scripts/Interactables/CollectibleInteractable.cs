using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectibleInteractable : Interactable
{
    public CollectibleObject _item;

    public TextMeshProUGUI textPrefab;

    public FloatingText floatingText;

    Color highlightColor;

    protected override void Start()
    {
        CreateCanvas();
        
        base.Start();
        if(_item.HasToPay)
            Debug.Log("hasToPay");
        else{
            Debug.Log(gameObject);

            //TextMeshProUGUI text = Instantiate(textPrefab, transform);
            //text.text = "test";
            //FloatingText ft = Instantiate(floatingText, text.transform);
            Debug.Log("It is to collect");
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

        AddTextToCanvas(myGO, _item.Name);
    }

    public void AddTextToCanvas(GameObject myGO, string name)
    {
        TextMeshProUGUI text;
        GameObject myText;

        //RectTransform rectTransform;
        FloatingText floatingText;

        ColorUtility.TryParseHtmlString("#FED600", out highlightColor);

        // Text
        myText = new GameObject();
        myText.transform.parent = myGO.transform;
        myText.name = "itemName";

        text = myText.AddComponent<TextMeshProUGUI>();
        text.text = name;
        text.fontSize = 36;
        text.color = highlightColor;

        // Text position
        //rectTransform = text.GetComponent<RectTransform>();
        //rectTransform.localPosition = new Vector3(0, 0, 0);
        //rectTransform.sizeDelta = new Vector2(400, 200);

        floatingText = myText.AddComponent<FloatingText>();
    }

    public override void Interact()
    {
        _player.Inventory.AddItem(_item, 1);
        Destroy(gameObject);
        _missionController.CheckIfItemCollectedIsMyGoal(_item);
    }
}
