using UnityEngine;


public class Item : MonoBehaviour
{
    [SerializeField]
    private ItemSO itemScriptableObject;
    [SerializeField]
    private int quantity;
    [SerializeField]
    private InventorySO playerInventory;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bag"))
            addItemToBag();
    }

    public void addItemToBag()
    {
        playerInventory.addItem(itemScriptableObject, quantity);
        this.gameObject.SetActive(false);
    }

}
