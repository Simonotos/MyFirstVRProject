using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private InventoryView inventoryView;
    [SerializeField]
    private InventorySO inventorySO;
    [SerializeField]
    private DescriptionView descriptionView;

    [SerializeField]
    private GameObject viewport;
    [SerializeField]
    private Scrollbar scrollbar;

    private void Start()
    {
        inventoryView.initializeInventoryUIPool(inventorySO.size);
        initializeInventoryView();
        inventoryView.onDescriptionRequested += handleItemClicked; 
    }

    private void Update()
    {

        if (inventoryView.opened)
        {
            foreach (var item in inventorySO.getCurrentInventoryState())
            {
                if (!item.Value.isEmpty())
                {
                    inventoryView.updateItemUI(item.Key, item.Value.itemSO.Image, item.Value.quantity, item.Value.itemSO.name);
                }
                else
                {
                    inventoryView.updateItemToEmpty(item.Key);
                }
            }
        }
    }

    private void handleItemClicked(int itemIndex)
    {
        InventoryItem item = inventorySO.getItemAt(itemIndex);
        ItemSO itemSO = item.itemSO;
        inventoryView.updateDescription(itemIndex, itemSO.Image,
            itemSO.Name, itemSO.Description);
    }

    public void openCloseInventoryUI()
    {
        inventoryView.openCloseWindow();
    }

    public bool removeItem(ItemSO itemUsed)
    {
        return inventorySO.removeItem(itemUsed);
    }

    public void initializeInventoryView() {
        
        foreach (var item in inventorySO.getCurrentInventoryState())
        {
            inventoryView.updateItemUI(item.Key, item.Value.itemSO.Image, item.Value.quantity, item.Value.itemSO.name);
        }
    }
}
