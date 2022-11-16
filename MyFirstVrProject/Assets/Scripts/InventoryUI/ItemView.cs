using UnityEngine;
using UnityEngine.UI;
using System;

public class ItemView : MonoBehaviour
{
    public Image itemImage;
    public Text itemQuantity, itemName;
    public int pos_x, pos_y;

    public event Action<ItemView> OnItemClicked;

    public void setEmptyData()
    {
        itemImage.gameObject.SetActive(false);
        itemQuantity.text = "";
        this.itemName.text = "";
    }

    public void setData(Sprite itemImage, int itemQuantity, string itemName)
    {
        this.itemImage.gameObject.SetActive(true);
        this.itemImage.sprite = itemImage;

        this.itemQuantity.text = itemQuantity + "";

        this.itemName.text = itemName;
    }

    public void onItemClicked()
    {
        if(itemName.text != "")
            OnItemClicked?.Invoke(this);
    }

    public void setCoordinates(int x, int y)
    {
        pos_x = x;
        pos_y = y;
    }

    public int getX()
    {
        return pos_x;
    }

    public int getY()
    {
        return pos_y;
    }
}
