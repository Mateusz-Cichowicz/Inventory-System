using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject itemPrefab;
    public GameObject descritptionDisplay;
    private PlayerInventory playerInventory;
    private void Start()
    {
        // Get a reference to the PlayerInventory component
        playerInventory = FindObjectOfType<PlayerInventory>();
        
        playerInventory.OnInventoryCountChanged += AddRemoveItem;
        if (playerInventory == null)
        {
            Debug.LogError("PlayerInventory component not found.");
            return;
        }
        if (itemsParent == null)
        {
            Debug.LogError("ItemsParent component not found.");
            return;
        }
        // Populate the inventory UI
        PopulateInventoryUI();
    }
    private void PopulateInventoryUI()
    {
        // Iterate through the player's items and create UI elements for each item
        for (int i = 0; i < playerInventory.GetItemCount(); i++)
        {
            Item item = playerInventory.GetItemAtIndex(i);

            if (itemsParent.Find(item.Name))
            {
                ChangeQuantity(item, 1);
                continue;
            }

            InstantiateItem(item);
        }
    }

    private void InstantiateItem(Item item) 
    {
        GameObject itemObj = Instantiate(itemPrefab, itemsParent);

        itemObj.name = item.Name;
        Item itemObjValues = itemObj.GetComponent<Item>();
        itemObjValues.Name = item.Name;
        itemObjValues.Icon = item.Icon;
        itemObjValues.Description = item.Description;

        Button[] btnList = itemObj.GetComponentsInChildren<Button>();
        Button btnIcon = btnList[1].GetComponent<Button>();
        Image image = btnIcon.GetComponent<Image>();
        image.sprite = item.Icon;

        // Set the button's label or image to represent the item
        TMP_Text btnIconText = btnIcon.GetComponentInChildren<TMP_Text>();
        btnIconText.text = item.Name;

        // Add a click event to the button to display item details
        btnIcon.onClick.AddListener(() =>
        {
            ShowItemDetails(item);
        });

        Button btnRemove = btnList[0].GetComponent<Button>();
        btnRemove.onClick.AddListener(() =>
        {
            playerInventory.RemoveItem(item);
        });
    }

    private void ChangeQuantity(Item item, int amount)
    {
        Transform tempItemTransform = itemsParent.Find(item.Name);
        Item tempItem = tempItemTransform.GetComponent<Item>();
        tempItem.Quantity += amount;
        if (tempItemTransform.GetComponentInChildren<TMP_Text>()) {
            tempItemTransform.GetComponentInChildren<TMP_Text>().text = tempItem.Quantity.ToString();
        }
        if (tempItem.Quantity == 0) 
        {
            Destroy(tempItemTransform.gameObject);
        }
    }

    private void AddRemoveItem(Item item, int amount) 
    {
        if (itemsParent.Find(item.Name))
        {
            ChangeQuantity(item, amount);
        }
        else 
        {
            InstantiateItem(item);
        }
    }
    private void ShowItemDetails(Item item)
    {
        descritptionDisplay.SetActive(true);
        TMP_Text description = descritptionDisplay.GetComponentInChildren<TMP_Text>();
        description.text = item.Description;
    }
}
