using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public Transform ItemContent;
    public GameObject InventoryItem;
    public List<Collectible> Collectibles = new List<Collectible>();
    public Toggle EnableRemove;
    public InventoryItemController[] InventoryItems;

    private void Awake()
    {
        Instance = this;
    }

    public void Add(Collectible collectible)
    {
        Collectibles.Add(collectible);
    }

    public void Remove(Collectible collectible)
    {
        Collectibles.Remove(collectible);
    }

    public void ListItems()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in Collectibles)
        {
            Debug.Log(item);
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var removeButton = obj.transform.Find("RemoveButton").GetComponent<Button>();
            //var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

            itemName.text = item.name;
            //itemIcon.sprite = item.icon;

            if(EnableRemove.isOn)
            {
                removeButton.gameObject.SetActive(true);
            }
        }

        SetInventoryItems();
    }

    public void EnableItemsRemove()
    {
        if (EnableRemove.isOn)
        {
            foreach (Transform item in ItemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (Transform item in ItemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(false);
            }
        }
    }

    public void SetInventoryItems()
    {
        InventoryItems = ItemContent.GetComponentsInChildren<InventoryItemController>();
    
        for(int i=0; i < Collectibles.Count; i++)
        {
            InventoryItems[i].AddItem(Collectibles[i]);
        }
    }
}
