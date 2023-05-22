using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public ItemDatabase itemDatabase;


    public string GetItemName(int index) 
    {
        if (index >= 0 && index < itemDatabase.items.Length) 
        {
            return itemDatabase.items[index].Name;
        }
        else 
        {
            Debug.LogError("Invalid item index" + index);
            return string.Empty;
        }
    }
    public string GetItemDescription(int index)
    {
        if (index >= 0 && index < itemDatabase.items.Length)
        {
            return itemDatabase.items[index].Description;
        }
        else
        {
            Debug.LogError("Invalid item index" + index);
            return string.Empty;
        }
    }
    public int GetItemQuantity(int index)
    {
        if (index >= 0 && index < itemDatabase.items.Length)
        {
            return itemDatabase.items[index].Quantity;
        }
        else
        {
            Debug.LogError("Invalid item index" + index);
            return 0;
        }
    }
    public Sprite GetItemIcon(int index)
    {
        if (index >= 0 && index < itemDatabase.items.Length)
        {
            return itemDatabase.items[index].Icon;
        }
        else
        {
            Debug.LogError("Invalid item index" + index);
            return null;
        }
    }
}
