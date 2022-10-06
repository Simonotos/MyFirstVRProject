using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;


public class TeleportManager : MonoBehaviour
{
    public XRController rightController;
    public InputHelpers.Button teleportActivationButton;
    private float activationThreshold = 0.1f;

    public Text log;

    // Update is called once per frame
    void Update()
    {
        if (rightController)
        {
            rightController.gameObject.SetActive(checkTeleportActive(rightController));
        }
            
    }

    public bool checkTeleportActive(XRController controller){
        InputHelpers.IsPressed(controller.inputDevice, teleportActivationButton, out bool isPressed, activationThreshold);
        return isPressed;
    }
}
