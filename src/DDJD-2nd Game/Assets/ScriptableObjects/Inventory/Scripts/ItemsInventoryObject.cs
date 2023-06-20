using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

[CreateAssetMenu(
    fileName = "Inventory",
    menuName = "Scriptable Objects/Inventory System/Inventory"
)]
public class ItemsInventoryObject : ScriptableObject
{
    public List<ItemStack> Container = new List<ItemStack>();

    [SerializeField]
    private int _gold;

    public int Gold
    {
        get { return _gold; }
    }

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

    public void RemoveItem(CollectibleObject item)
    {
        ItemStack slot = Container.Find(x => x.item == item);
        if (slot == null)
            return;

        if (slot.RemoveAmount(1))
        {
            Container.Remove(slot);
        }
    }

    public void AddItem(ItemStack itemStack)
    {
        ItemStack slot = Container.Find(x => x.item == itemStack.item);

        if (slot != null)
        {
            slot.AddAmount(itemStack.amount);
        }
        else
        {
            Container.Add(itemStack);
        }
    }

    public void AddGold(int gold)
    {
        if(_gold + gold <= 9999)
            _gold += gold;
    }

    public void UsePotion(Player player)
    {
        ItemStack slot = Container.Find(x => x.item is Potion);
        if (slot == null)
            return;
        if (player.Status.HasMaxHealth())
            return;

        (slot.item as Potion).Use(player);
        if (slot.RemoveAmount(1))
        {
            Container.Remove(slot);
        }
    }

    public bool SubGold(int gold)
    {
        if(_gold < gold)
            return false;
        _gold -= gold;
        return true;
    }
}

[System.Serializable]
public class ItemStack
{
    public ItemObject item;
    public int amount;
    public string id;

    public ItemStack(ItemObject _item, int _amount, string id = null)
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

    public bool RemoveAmount(int value)
    {
        amount -= value;
        return amount <= 0;
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
