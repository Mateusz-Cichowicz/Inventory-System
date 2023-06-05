using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject itemPrefab;
    public GameObject DescritptionDisplay;
    private PlayerInventory playerInventory;
    private void Start()
    {
        // Get a reference to the PlayerInventory component
        playerInventory = FindObjectOfType<PlayerInventory>();
        playerInventory.OnInventoryCountChanged += PopulateInventoryUI;
        // Populate the inventory UI
        PopulateInventoryUI();
    }
    private void PopulateInventoryUI()
    {
        // Clear existing items in the UI
        foreach (Transform child in itemsParent)
        {
            Destroy(child.gameObject);
        }

        // Iterate through the player's items and create UI elements for each item
        for (int i = 0; i < playerInventory.GetItemCount(); i++)
        {
            Item item = playerInventory.GetItemAtIndex(i);
            GameObject itemObj = Instantiate(itemPrefab, itemsParent);
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

    }
    private void ShowItemDetails(Item item)
    {
        DescritptionDisplay.SetActive(true);
        TMP_Text description = DescritptionDisplay.GetComponentInChildren<TMP_Text>();
        description.text = item.Description;
    }
}
