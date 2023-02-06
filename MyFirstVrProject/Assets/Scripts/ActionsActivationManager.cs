using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class ActionsActivationManager : MonoBehaviour
{
    [SerializeField]
    private GameObject teleportRayR, teleportRayL, grabRayR, grabRayL;
    public InputActionReference teleportActivationButtonR, teleportActivationButtonL, grabbableByDistanceActivationButtonR, grabbableByDistanceActivationButtonL;
    private bool teleportR = false, teleportL = false, grabR = false, grabL = false;

    public bool holdingObjectR = false, holdingObjectL = false;

    public event Action onHoldingObject;

    private void Awake()
    {
        teleportActivationButtonR.action.started += onRightTeleportActivated;
        teleportActivationButtonL.action.started += onLeftTeleportActivated;
        grabbableByDistanceActivationButtonR.action.started += onRightDistanceGrabActivated;
        grabbableByDistanceActivationButtonL.action.started += onLeftDistanceGrabActivated;
        onHoldingObject += disactivateRays;
    }

    public void invokeAction()
    {
       onHoldingObject?.Invoke();
    }

    public void disactivateRays()
    {
        if (holdingObjectR)
        {
            teleportRayR.gameObject.SetActive(false);
            teleportR = false;
            grabRayR.gameObject.SetActive(false);
            grabR = false;
        }

        if (holdingObjectL)
        {
            teleportRayL.gameObject.SetActive(false);
            teleportL = false;
            grabRayL.gameObject.SetActive(false);
            grabL = false;
        }
    }

    public void onRightTeleportActivated(InputAction.CallbackContext context)
    {
        if (context.started && !holdingObjectR)
        {
            teleportR = !teleportR;
            grabR = false;
            teleportRayR.SetActive(teleportR);

            if(grabRayR.activeInHierarchy)
                grabRayR.SetActive(false);
        }
    }

    public void onRightDistanceGrabActivated(InputAction.CallbackContext context)
    {
        if (context.started && !holdingObjectR)
        {
            grabR = !grabR;
            teleportR = false;
            grabRayR.SetActive(grabR);

            if (teleportRayR.activeInHierarchy)
                teleportRayR.SetActive(false);
        }
    }

    public void onLeftTeleportActivated(InputAction.CallbackContext context)
    {
        if (context.started && !holdingObjectL)
        {
            teleportL = !teleportL;
            grabL = false;
            teleportRayL.SetActive(teleportL);

            if (grabRayL.activeInHierarchy)
                grabRayL.SetActive(false);
        }
    }

    public void onLeftDistanceGrabActivated(InputAction.CallbackContext context)
    {
        if (context.started && !holdingObjectL)
        {
            grabL = !grabL;
            teleportL = false;
            grabRayL.SetActive(grabL);

            if (teleportRayL.activeInHierarchy)
                teleportRayL.SetActive(false);
        }
    }

}
