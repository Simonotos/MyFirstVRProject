using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class ItemView : MonoBehaviour
{
    public Image itemImage;
    public Text itemQuantity, itemName;

    public event Action<ItemView> OnItemClicked;

    public void setData(Sprite itemImage, int itemQuantity, string itemName)
    {
        this.itemName.text = itemName;
        this.itemImage.sprite = itemImage;
        this.itemQuantity.text = itemQuantity + "";
    }

    public void onItemClicked()
    {
        OnItemClicked?.Invoke(this);
    }
}
