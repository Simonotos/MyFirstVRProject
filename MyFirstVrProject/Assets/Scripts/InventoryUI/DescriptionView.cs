using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionView : MonoBehaviour
{
    [SerializeField]
    private Image itemImage;
    [SerializeField]
    private Text itemName;
    [SerializeField]
    private Text itemDescription;

    private void Awake()
    {
        resetDescription();
    }

    public void resetDescription()
    {
        itemImage.gameObject.SetActive(false);
        itemDescription.text = "";
        itemName.text = "";
    }

    public void setDescription(Sprite itemImage, 
        string itemName, string itemDescription)
    {
        this.itemImage.gameObject.SetActive(true);
        this.itemImage.sprite = itemImage;
        this.itemName.text = itemName;
        this.itemDescription.text = itemDescription;
    }
}
