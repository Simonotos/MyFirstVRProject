using UnityEngine;
using UnityEngine.InputSystem;

public class TeleportManager : MonoBehaviour
{
    [SerializeField]
    private GameObject teleportRay;
    public InputActionReference teleportActivationButton;
    private bool activated = false;

    // Update is called once per frame
    void Update()
    {

        if (teleportActivationButton.action.triggered)
        {
            activated = !activated;
            teleportRay.gameObject.SetActive(activated);
        }
 

    }
}
