using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryView : MonoBehaviour
{
    [SerializeField]
    private RectTransform contentPanel;

    [SerializeField]
    private ItemView itemUIPrefab;

    [SerializeField]
    private GameObject myCanvas;

    [SerializeField]
    private DescriptionView descriptionView;

    private List<ItemView> listOfUIItems = new List<ItemView>();

    private bool opened = false;

    public Action<int> onDescriptionRequested;

    private void Awake()
    {
        descriptionView.resetDescription();
    }

    public void openCloseWindow()
    {
        opened = !opened;
        myCanvas.SetActive(opened);
    }

    public void updateUIData(int itemIndex, Sprite itemImage, int itemQuantity, string itemName)
    {
        if (listOfUIItems.Count > itemIndex)
            listOfUIItems[itemIndex].setData(itemImage, itemQuantity, itemName);
        else
        {
            ItemView item = Instantiate(itemUIPrefab, Vector3.zero, Quaternion.identity);
            item.transform.SetParent(contentPanel, false);
            item.setData(itemImage, itemQuantity, itemName);
            listOfUIItems.Add(item);
            //event handler
            item.OnItemClicked += OnItemClicked;
        } 
    }

    private void OnItemClicked(ItemView obj)
    {
        int index = listOfUIItems.IndexOf(obj);

        if (index == -1)
            return;

        onDescriptionRequested.Invoke(index);
    }

    public void updateDescription(int itemIndex, Sprite itemImage, string itemName, string itemDescription)
    {
        descriptionView.setDescription(itemImage, itemName, itemDescription);
    }
}
