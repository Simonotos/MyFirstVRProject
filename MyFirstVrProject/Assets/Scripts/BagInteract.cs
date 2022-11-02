using UnityEngine;
using UnityEngine.InputSystem;

public class BagInteract : MonoBehaviour
{
    public InputActionReference triggerPressedR, triggerPressedL;

    [SerializeField]
    private InventoryController inventoryController;

    public bool enteredBagR = false, enteredBagL = false;

    private void Update()
    {
        triggerPressedR.action.canceled += onTriggerReleasedR;
        triggerPressedL.action.canceled += onTriggerReleasedL;
    }


    private void onTriggerReleasedR(InputAction.CallbackContext context)
    {
        if (context.canceled && enteredBagR)
            openCloseInventory(); 
    }

    private void onTriggerReleasedL(InputAction.CallbackContext context)
    {
        if (context.canceled && enteredBagL)
            openCloseInventory();
    }

    private void openCloseInventory()
    {
        inventoryController.openCloseInventoryUI();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Right Hand"))
        {
            enteredBagR = true;
            enteredBagL = false;
        }

        if (other.CompareTag("Left Hand"))
        {
            enteredBagL = true;
            enteredBagR = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Right Hand"))
            enteredBagR = false;

        if (other.CompareTag("Left Hand"))
            enteredBagL = false;
    }
}
