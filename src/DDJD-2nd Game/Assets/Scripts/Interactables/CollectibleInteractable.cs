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

    private static string assetSaveLocation = "Assets/Materials/Outline/Outline.mat";

    private Material _material;
    private MeshRenderer _meshRenderer;
    Color highlightColor;

    protected override void Start()
    {
        base.Start();
        if (_item.Purchasable)
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
        if (_item.Purchasable)
        {
            HelpManager.Instance.SetHelpText("Press F to purchase");
            ActivateHighlight();
        }
        else
        {   
            HelpManager.Instance.SetHelpText("Press F to collect");
        }
    }

    private void ActivateHighlight()
    {
        if(!_material)
        {
            _material = (Material)AssetDatabase.LoadAssetAtPath(assetSaveLocation , typeof(Material));
            _meshRenderer = GetComponent<MeshRenderer>();
        }
            
        Material[] materials = _meshRenderer.materials;
        Material[] newMaterials = new Material[materials.Length + 1];
        materials.CopyTo(newMaterials, 0);
        newMaterials[newMaterials.Length - 1] = _material;

        _meshRenderer.materials = newMaterials;
    }

    public override void EndInteract()
    {
        base.EndInteract();

        if (_item.Purchasable && _meshRenderer.materials.Length >= 2){
            DeactivateHighlight();
        }
    }

    private void DeactivateHighlight()
    {
        Material[] materials = _meshRenderer.materials;
        Material[] newMaterials = new Material[materials.Length - 1];

        for (int i = 0; i < newMaterials.Length; i++)
        {
            newMaterials[i] = materials[i];
        }

        _meshRenderer.materials = newMaterials;
    }

    public override void Interact()
    {
        if (!_item.Purchasable)
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
