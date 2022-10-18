using UnityEngine;
using UnityEngine.InputSystem;

public class ActionsActivationManager : MonoBehaviour
{
    [SerializeField]
    private GameObject teleportRayR, teleportRayL, grabRayR, grabRayL;
    public InputActionReference teleportActivationButtonR, teleportActivationButtonL, grabbableByDistanceActivationButtonR, grabbableByDistanceActivationButtonL;
    private bool teleportR = false, teleportL = false, grabR = false, grabL = false;

    private void Start()
    {
        teleportRayR.gameObject.SetActive(false);
        teleportRayL.gameObject.SetActive(false);
        grabRayR.gameObject.SetActive(false);
        grabRayL.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        teleportActivationButtonR.action.performed += onRightTeleportActivated;
        teleportActivationButtonL.action.performed += onLeftTeleportActivated;
        grabbableByDistanceActivationButtonR.action.performed += onRightDistanceGrabActivated;
        grabbableByDistanceActivationButtonL.action.performed += onLeftDistanceGrabActivated;
    }

    public void onRightTeleportActivated(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            teleportR = !teleportR;
            grabR = false;
            teleportRayR.SetActive(teleportR);
            grabRayR.SetActive(false);
        }
    }

    public void onRightDistanceGrabActivated(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            grabR = !grabR;
            teleportR = false;
            grabRayR.SetActive(grabR);
            teleportRayR.SetActive(false);
        }
    }

    public void onLeftTeleportActivated(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            teleportL = !teleportL;
            grabL = false;
            teleportRayL.SetActive(teleportL);
            grabRayL.SetActive(false);
        }
    }

    public void onLeftDistanceGrabActivated(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            grabL = !grabL;
            teleportL = false;
            grabRayL.SetActive(grabL);
            teleportRayL.SetActive(false);
        }
    }
}
