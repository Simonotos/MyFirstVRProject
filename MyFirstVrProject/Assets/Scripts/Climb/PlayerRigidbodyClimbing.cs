using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerRigidbodyClimbing : MonoBehaviour
{
    [SerializeField]
    private Pull rightHandPulling, leftHandPulling;
    private Rigidbody myRigidbody;

    public bool isGrounded;

    [SerializeField]
    private float sphereRadius;
    [SerializeField]
    private LayerMask terrainLayers;
    private bool startedClimbing;

    [SerializeField]
    private MeshRenderer gunSocket, bag;
    [SerializeField]
    private GameObject gun;
    [SerializeField]
    private Canvas gunHUID;
    [SerializeField]
    private SpriteRenderer gunTargetSprite;

    [SerializeField]
    private AudioClip breathingClip, breathingFasterClip;
    private AudioSource myAudioSource;
    private bool rightHandTired, leftHandTired;

    private void Awake()
    {
        rightHandTired = false; leftHandTired = false;
        myAudioSource = GetComponent<AudioSource>();
        isGrounded = true;
        startedClimbing = false;
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rightHandPulling.onGrabbing || leftHandPulling.onGrabbing)
        {
            startedClimbing = true;
            HideShowObjects(false);
            handleRigidody(false);
        }
        else
        {
            startedClimbing = false;
            HideShowObjects(true);
            handleRigidody(true);
        }
    }

    private void FixedUpdate()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, sphereRadius, terrainLayers);

        if (hitColliders.Length > 0)
            isGrounded = true;
        else
            isGrounded = false;
    }

    /*private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, sphereRadius);
    }*/

    private void HideShowObjects(bool value)
    {
        gun.SetActive(value);
        gunTargetSprite.enabled = value;
        gunHUID.enabled = value;
        gunSocket.enabled = value;
        bag.enabled = value;
    }
    private void handleRigidody(bool activate)
    {
        myRigidbody.useGravity = activate;
    }

    public void breathingFaster(bool value, int handType)
    {
        if (handType == 1)
            rightHandTired = value;
        else
            leftHandTired = value;

        if (value)
            myAudioSource.clip = breathingFasterClip;
        else if(!rightHandTired && !leftHandTired)
            myAudioSource.clip = breathingClip;

        if(!myAudioSource.isPlaying)
            myAudioSource.Play();
    }

}
