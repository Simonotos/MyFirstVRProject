using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InventorySO : ScriptableObject
{
    [SerializeField]
    private List<InventoryItem> inventoryItems;
    public int size;

    public void addItem(ItemSO itemSO, int quantity)
    {
        bool finded = false;

        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].itemSO.ID == itemSO.ID) //same object
            {
                //check if stackable
                if (itemSO.isStackable)
                {
                    int newQuantity = inventoryItems[i].quantity + quantity;

                    if(newQuantity <= inventoryItems[i].itemSO.maxStackable)
                    {
                        finded = true;
                        inventoryItems[i] = inventoryItems[i].changeQuantity(itemSO, newQuantity);
                    }
                }
                else
                    break;
            }
        }

        if (!finded)
            inventoryItems.Add(new InventoryItem { itemSO = itemSO, quantity = quantity, });

        updateSize();
    }

    public Dictionary<int, InventoryItem> getCurrentInventoryState()
    {
        Dictionary<int, InventoryItem> returnedValue = new Dictionary<int, InventoryItem>();

        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (!inventoryItems[i].isEmpty())
                returnedValue[i] = inventoryItems[i];
        }

        return returnedValue;
    }

    public void updateSize()
    {
        size = inventoryItems.Count;
    }
    public void printArray()
    {
        string output = "";

        foreach (var item in inventoryItems)
        {
            output += " " + item.itemSO.Name + " " + item.quantity + "/";
        }

        Debug.Log(output);
    }

    public InventoryItem getItemAt(int index)
    {
        return inventoryItems[index];
    }
}

[Serializable]
public struct InventoryItem //struct safer instead of class
{
    public ItemSO itemSO;
    public int quantity;

    public InventoryItem changeQuantity(ItemSO sameItemSO, int newQuantity)
    {
        return new InventoryItem{itemSO = sameItemSO, quantity = newQuantity};
    }

    public bool isEmpty()
    {
        if (!itemSO)
            return true;
        
        return false;
    }
}
