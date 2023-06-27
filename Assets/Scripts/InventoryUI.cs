using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject itemPrefab;
    public GameObject descritptionDisplay;
    private PlayerInventory playerInventory;
    [SerializeField]
    private List<Sprite> sprites;
    private void Start()
    {
        // Get a reference to the PlayerInventory component
        playerInventory = FindObjectOfType<PlayerInventory>();
        
        if (playerInventory == null)
        {
            Debug.LogError("PlayerInventory component not found.");
            return;
        }

        playerInventory.OnInventoryCountChanged += AddRemoveItem;
        playerInventory.OnInventorySorted += StartPopulateInventoryCoroutine;

        if (itemsParent == null)
        {
            Debug.LogError("ItemsParent component not found.");
            return;
        }
        // Populate the inventory UI
        StartCoroutine(PopulateInventoryUI());
    }

    private void StartPopulateInventoryCoroutine() 
    {
        StartCoroutine(PopulateInventoryUI());
    }
    IEnumerator PopulateInventoryUI()
    {
        foreach (Transform obj in itemsParent)
        {
            Destroy(obj.gameObject);
        }
        yield return new WaitForEndOfFrame();
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

        Button[] btnList = itemObj.GetComponentsInChildren<Button>();
        Button btnIcon = btnList[1].GetComponent<Button>();
        Image image = btnIcon.GetComponent<Image>();
        image.sprite = sprites.Find(x => x.name == item.Name);

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


        if (tempItemTransform.GetComponentInChildren<TMP_Text>()) {
            TMP_Text text = tempItemTransform.GetComponentInChildren<TMP_Text>();
            int i;
            Int32.TryParse(text.text, out i);
            i += amount;
            text.text = i.ToString();
        }
        if (tempItemTransform.GetComponentInChildren<TMP_Text>().text == "0") 
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
