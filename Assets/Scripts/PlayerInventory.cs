using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json;

public class PlayerInventory : MonoBehaviour
{
    public Inventory inventoryItems = new Inventory();
    public ItemDatabase dataBase;
    public event System.Action<Item, int> OnInventoryCountChanged;
    public event System.Action OnInventorySorted;
    private string jsonData;

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) 
        {
            AddRandomItem();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) { SaveInventory(); }
        if (Input.GetKeyDown(KeyCode.Alpha5)) { LoadInventory(); }
    }
    public void AddItem(Item item)
    {
        inventoryItems.items.Add(item);
    }
    public void AddRandomItem() 
    {
        Item item = dataBase.items[Random.Range(0, dataBase.items.Length)];
        inventoryItems.items.Add(item);
        OnInventoryCountChanged?.Invoke(item, 1);
    }

    public void RemoveItem(Item item)
    {
        inventoryItems.items.Remove(item);
        OnInventoryCountChanged?.Invoke(item, -1);
    }

    public Item GetItemAtIndex(int index)
    {
        if (index >= 0 && index < inventoryItems.items.Count)
        {
            return inventoryItems.items[index];
        }

        return null;
    }
    public int GetItemCount()
    {
        return inventoryItems.items.Count;
    }

    public void SortInventory() 
    {
        inventoryItems.items = inventoryItems.items.OrderBy(x => x.Name).ToList();
        OnInventorySorted.Invoke();
    }

    void SaveInventory()
    {
        Debug.Log("Saving data");
        jsonData = JsonConvert.SerializeObject(inventoryItems);
        PlayerPrefs.SetString("inventory", jsonData);
    }
    void LoadInventory()
    {
        Debug.Log("Loading data");
        jsonData = PlayerPrefs.GetString("inventory");
        inventoryItems = JsonConvert.DeserializeObject<Inventory>(jsonData);
        OnInventorySorted.Invoke();
    }
}
