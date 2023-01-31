using UnityEngine;

public class HideShowObjects : MonoBehaviour
{
    private bool canShowObjects;
    [SerializeField]
    private bool island1, island2;

    //Island1
    [SerializeField]
    private Canvas leftHandHUID, rightHandHUID;
    [SerializeField]
    private PlayerRigidbodyClimbing playerRgbClimbing;
    [SerializeField]
    private AudioSource playerAudioSource;
    //

    [Space(20)]

    //Island2
    [SerializeField]
    private GameObject gunSocket;
    [SerializeField]
    private GameObject gun;
    //

    private void showObjects()
    {
        if (island1) { 
            leftHandHUID.enabled = canShowObjects; rightHandHUID.enabled = canShowObjects;
            playerRgbClimbing.enabled = canShowObjects;
            playerAudioSource.enabled = canShowObjects;
        }

        if (island2)
        {
            gunSocket.SetActive(canShowObjects);
            gun.SetActive(canShowObjects);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canShowObjects = true;
            showObjects();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canShowObjects = false;
            showObjects();
        }
    }
}
