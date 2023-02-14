using UnityEngine;

public class HideShowObjects : MonoBehaviour
{
    [SerializeField]
    private bool canShowObjects1 = false, canShowObjects2 = false, canShowObjects3 = false, saveShowObjects = false;

    //Island1
    [SerializeField]
    private Canvas leftHandHUID, rightHandHUID;
    [SerializeField]
    private Pull rightHandPull, leftHandPull;
    [SerializeField]
    private PlayerRigidbodyClimbing playerRgbClimbing;
    [SerializeField]
    private AudioSource playerAudioSource;
    //

    [Space(20)]

    //Island2
    [SerializeField]
    private GameObject gunModel, gunSocket;
    [SerializeField]
    private XRGrabInteractableTwoAttach gunInteractableScript;
    //

    //Island3
    [SerializeField]
    private GameObject inventoryBag;

    private void Update()
    {
        if (canShowObjects1 != saveShowObjects || canShowObjects2 != saveShowObjects || (canShowObjects1 == canShowObjects2 && saveShowObjects))
        {
            if(canShowObjects1 != canShowObjects2)
                saveShowObjects = true;

            //island1
            leftHandHUID.enabled = canShowObjects1; rightHandHUID.enabled = canShowObjects1;
            playerRgbClimbing.enabled = canShowObjects1;
            playerAudioSource.enabled = canShowObjects1;
            rightHandPull.enabled = canShowObjects1; leftHandPull.enabled = canShowObjects1;

            //island2
            gunModel.SetActive(canShowObjects2);
            gunSocket.SetActive(canShowObjects2);
            gunInteractableScript.enabled = canShowObjects2;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Island1"))
        {
            saveShowObjects = canShowObjects1;
            canShowObjects1 = true;
        }

        if (other.CompareTag("Island2"))
        {
            saveShowObjects = canShowObjects2;
            canShowObjects2 = true;
        }

        if (other.CompareTag("Island3"))
        {
            inventoryBag.SetActive(false);
        }

        if (other.CompareTag("IslandHub"))
        {
            canShowObjects1 = false; canShowObjects2 = false;
            inventoryBag.SetActive(true);
        }
    }
}
