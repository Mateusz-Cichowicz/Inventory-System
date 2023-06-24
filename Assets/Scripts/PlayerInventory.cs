using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    private List<Item> inventoryItems = new List<Item>();
    public ItemDatabase dataBase;
    public event System.Action<Item, int> OnInventoryCountChanged;
    public event System.Action OnInventorySorted;
    private void Start()
    {
        AddItem(dataBase.items[1]);
        AddItem(dataBase.items[0]);
        AddItem(dataBase.items[0]);
        AddItem(dataBase.items[1]);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) 
        {
            AddRandomItem();
        }
    }
    public void AddItem(Item item)
    {
        inventoryItems.Add(item);
    }
    public void AddRandomItem() 
    {
        Item item = dataBase.items[Random.Range(0, dataBase.items.Length)];
        inventoryItems.Add(item);
        OnInventoryCountChanged?.Invoke(item, 1);
    }

    public void RemoveItem(Item item)
    {
        inventoryItems.Remove(item);
        OnInventoryCountChanged?.Invoke(item, -1);
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

    public void SortInventory() 
    {
        inventoryItems = inventoryItems.OrderBy(x => x.Name).ToList();
        OnInventorySorted.Invoke();
    }
}
