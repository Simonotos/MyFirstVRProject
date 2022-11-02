using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private InventoryView inventoryView;
    [SerializeField]
    private InventorySO inventoryData;
    [SerializeField]
    private DescriptionView descriptionView;

    private void Awake()
    {
        inventoryView.onDescriptionRequested += handleItemClicked;
    }

    private void handleItemClicked(int itemIndex)
    {
        InventoryItem item = inventoryData.getItemAt(itemIndex);
        ItemSO itemSO = item.itemSO;
        inventoryView.updateDescription(itemIndex, itemSO.Image, itemSO.Name, itemSO.Description);
    }

    void Update()
    {
        foreach (var item in inventoryData.getCurrentInventoryState())
        {
            inventoryView.updateUIData(item.Key, item.Value.itemSO.Image, 
                item.Value.quantity, item.Value.itemSO.Name);
        }
    }

    public void openCloseInventoryUI()
    {
        inventoryView.openCloseWindow();
    }
}
