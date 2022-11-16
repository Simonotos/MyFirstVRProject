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

    public bool opened = false;

    public Action<int> onDescriptionRequested;

    public int offset_x, offset_y;
    public int elementsInRow;

    public void openCloseWindow()
    {
        opened = !opened;
        myCanvas.SetActive(opened);
    }

    public void initializeInventoryUIPool(int size)
    {
        int pos_x = offset_x;
        int pos_y = 15;

        for (int i = 0; i < size; i++)
        {
            if (i > 0)
            {
                if (i % 3 == 0) //ho finito la riga
                {
                    pos_x = offset_x;
                    pos_y += offset_y;
                }
                else{pos_x += offset_x * 2;}
            }

            ItemView item = Instantiate(itemUIPrefab, Vector3.zero, Quaternion.identity);
            item.transform.SetParent(contentPanel, false);
            item.setEmptyData();
            item.transform.localPosition = new Vector3(pos_x , -(pos_y), 0);
            item.setCoordinates(pos_x, pos_y);
            listOfUIItems.Add(item);
            //event handler
            item.OnItemClicked += OnItemClicked;
        }
    }

    public void updateItemUI(int itemIndex, Sprite itemImage, int itemQuantity, string itemName)
    {
         listOfUIItems[itemIndex].setData(itemImage, itemQuantity, itemName);
    }

    public void updateItemToEmpty(int itemIndex)
    {
        listOfUIItems[itemIndex].setEmptyData();
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
