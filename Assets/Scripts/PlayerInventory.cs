using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInventory : MonoBehaviour
{
    private List<Item> inventoryItems = new List<Item>();
    public ItemDatabase dataBase;
    private void Start()
    {
        AddItem(dataBase.items[0]);
        AddItem(dataBase.items[0]);
        AddItem(dataBase.items[0]);
        AddItem(dataBase.items[0]);
    }
    public void AddItem(Item item)
    {
        inventoryItems.Add(item);
    }

    public void RemoveItem(Item item)
    {
        inventoryItems.Remove(item);
    }

    public Item GetItemAtIndex(int index)
    {
        if (index >= 0 && index < inventoryItems.Count)
        {
            return inventoryItems[index];
        }

        return null;
    }

    public int GetItemCount()
    {
        return inventoryItems.Count;
    }
}
