using System;
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

    [SerializeField]
    private GameObject panel;

    public Action onUsageButtonClicked;

    private void Awake()
    {
        resetDescription();
    }

    public void resetDescription()
    {
        panel.SetActive(false);
    }

    public void setDescription(Sprite itemImage, 
        string itemName, string itemDescription)
    {
        this.itemImage.sprite = itemImage;
        this.itemName.text = itemName;
        this.itemDescription.text = itemDescription;
        panel.SetActive(true);
    }

    public void onButtonClicked()
    {
        onUsageButtonClicked.Invoke();
    }
}
