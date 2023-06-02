using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

[CreateAssetMenu(fileName= "Inventory", menuName = "Scriptable Objects/Inventory System/Inventory")]
public class ItemsInventoryObject : ScriptableObject
{
    public List<ItemStack> Container = new List<ItemStack>();
    public void AddItem(CollectibleObject item, int amount)
    {
        ItemStack slot = Container.Find(x => x.item == item);

        if (slot != null)
        {
            slot.AddAmount(amount);
        }
        else
        {
            Container.Add(new ItemStack(item, amount, null));
        }
    }
}

[System.Serializable]
public class ItemStack
{

    public ItemObject item;
    public int amount;
    public string id;

    public ItemStack(ItemObject _item, int _amount, string id)
    {
        item = _item;
        amount = _amount;
        if (id != null)
        {
            this.id = id;
        }
        else
        {
            id = GenerateRandomStringHash(10);
        }
    }

    public void AddAmount(int value)
    {
        amount += value;
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
