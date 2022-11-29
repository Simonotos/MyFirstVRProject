using System.Collections.Generic;
using UnityEngine;

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
    private ItemSO itemSOWatchingDescription;

    [SerializeField]
    private GameObject playerHead;

    [SerializeField]
    private List<ObjectPooling> poolList;

    private void Start()
    {
        inventoryView.initializeInventoryUIPool(inventorySO.size);
        initializeInventoryView();
        inventoryView.onDescriptionRequested += handleItemClicked;
        descriptionView.onUsageButtonClicked += handleUsageButtonClicked;
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

        itemSOWatchingDescription = itemSO;
    }

    private void handleUsageButtonClicked()
    {
        if (itemSOWatchingDescription != null)
        {
            foreach (var poolScript in poolList)
            {
                if (poolScript.itemSO.ID == itemSOWatchingDescription.ID)
                {
                    GameObject obj = poolScript.getPooledObject();
                    obj.transform.position = new Vector3(playerHead.transform.position.x, playerHead.transform.position.y, playerHead.transform.position.z + 0.3f);
                    obj.SetActive(true);
                    removeItem(itemSOWatchingDescription);
                    return;
                }
            }
        }
    }

    public void openCloseInventoryUI()
    {
        inventoryView.openCloseWindow();
    }

    public int removeItem(ItemSO itemUsed)
    {
        int value_returned = inventorySO.removeItem(itemUsed);

        if (value_returned == 0)
        {
            descriptionView.resetDescription();
            itemSOWatchingDescription = null;
        }

        return value_returned;
    }

    public void initializeInventoryView() {
        
        foreach (var item in inventorySO.getCurrentInventoryState())
        {
            inventoryView.updateItemUI(item.Key, item.Value.itemSO.Image, item.Value.quantity, item.Value.itemSO.name);
        }
    }
}
