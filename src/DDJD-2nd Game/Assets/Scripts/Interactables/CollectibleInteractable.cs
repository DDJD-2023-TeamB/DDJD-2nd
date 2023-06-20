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

    private static string outlineMaterialPath = "Assets/Materials/Outline/Outline.mat";
    private static string floatingTextPrefabPath = "Assets/Prefabs/UI/FloatingCanvas.prefab";

    private Material _material;
    private MeshRenderer _meshRenderer;

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
        GameObject floatingCanvasPrefab = (GameObject)
            AssetDatabase.LoadAssetAtPath(floatingTextPrefabPath, typeof(GameObject));
        GameObject floatingTextCanvas = Instantiate(floatingCanvasPrefab);

        TextMeshProUGUI text = floatingTextCanvas.transform
            .GetChild(0)
            .GetComponent<TextMeshProUGUI>();
        text.text = _item.Cost.ToString() + "$";

        floatingTextCanvas.transform.SetParent(transform, false);
    }

    protected override void Approach()
    {
        if (_item.Purchasable)
        {
            UpdateText();
            ActivateHighlight();
        }
        else
        {
            HelpManager.Instance.SetHelpText("Press F to collect");
        }
    }

    private void ActivateHighlight()
    {
        if (!_material)
        {
            _material = (Material)
                AssetDatabase.LoadAssetAtPath(outlineMaterialPath, typeof(Material));
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        Material[] materials = _meshRenderer.materials;
        Material[] newMaterials = new Material[materials.Length + 1];
        materials.CopyTo(newMaterials, 0);
        newMaterials[newMaterials.Length - 1] = _material;

        _meshRenderer.materials = newMaterials;
    }

    private void UpdateText()
    {
        HelpManager.Instance.ResetText();
        HelpManager.Instance.SetHelpText("Press F to purchase");
        if (!_item.Unlimited)
            HelpManager.Instance.AddText(_item.NumLeftToBuy + " " + _item.Name + " left");
    }

    public override void EndInteract()
    {
        base.EndInteract();

        if (_item.Purchasable && _meshRenderer.materials.Length >= 2)
        {
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
                _item.BuyItem();
                _player.Inventory.AddItem(_item, 1);
                UpdateText();

                if (_item.NumLeftToBuy == 0 && !_item.Unlimited)
                {
                    Destroy(gameObject);
                    HelpManager.Instance.ResetText();
                }
                    
            }
            else
            {
                HelpManager.Instance.SetHelpText("Not enough gold");
            }
        }
    }

    public override bool IsInstant()
    {
        return true;
    }

  
}
