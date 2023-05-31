using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject itemButtonPrefab;
    public GameObject DescritptionDisplay;
    private PlayerInventory playerInventory;
    private void Start()
    {
        // Get a reference to the PlayerInventory component
        playerInventory = FindObjectOfType<PlayerInventory>();
        playerInventory.OnInventoryCountChanged += UpdateItems;
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
            UpdateItems(item);
        }

    }
    private void UpdateItems(Item item) 
    {
        GameObject itemButton = Instantiate(itemButtonPrefab, itemsParent);
        Image image = itemButton.GetComponent<Image>();
        image.sprite = item.Icon;
        Button button = itemButton.GetComponent<Button>();

        // Set the button's label or image to represent the item
        TMP_Text buttonText = itemButton.GetComponentInChildren<TMP_Text>();
        buttonText.text = item.name;


        // Add a click event to the button to display item details
        button.onClick.AddListener(() =>
        {
            ShowItemDetails(item);

        });
    }
    private void ShowItemDetails(Item item)
    {
        DescritptionDisplay.SetActive(true);
        TMP_Text description = DescritptionDisplay.GetComponentInChildren<TMP_Text>();
        description.text = item.Description;
    }
}
