using UnityEngine;


public class Item : MonoBehaviour
{
    [SerializeField]
    private ItemSO itemScriptableObject;
    [SerializeField]
    private int quantity;
    [SerializeField]
    private InventorySO playerInventory;
    private GrabHandPose grabHand;

    void Awake()
    {
        grabHand = GetComponent<GrabHandPose>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bag"))
        {
            /*if (grabHand)
            {
                if(grabHand.handHoldingObject == 0)*/

            addItemToBag();
        }
    }

    public void addItemToBag()
    {
        playerInventory.addItem(itemScriptableObject, quantity);
        Destroy(this.gameObject);
    }

}
