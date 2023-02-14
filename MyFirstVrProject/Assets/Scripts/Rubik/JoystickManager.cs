using UnityEngine;
using UnityEngine.InputSystem;

public class JoystickManager : MonoBehaviour
{
    private Transform handR, handL;

    [SerializeField]
    private Transform rubikCube;
    [SerializeField]
    private float rubikRotationSpeed;

    //attivare/disattivare con Grab Premuto
    public InputActionReference onGrabR, onGrabL;
    private bool grabbingR = false, grabbingL = false;

    private void Awake()
    {
        onGrabR.action.started += onGrabbingR;
        onGrabR.action.canceled += releasedGrabR;

        onGrabL.action.started += onGrabbingL;
        onGrabL.action.canceled += releasedGrabL;
    }

    // Update is called once per frame
    void Update()
    {
        Transform hand = null;
        if (handR)
            hand = handR;
        if (handL)
            hand = handL;

        if (hand != null)
        {
            Vector3 direction = hand.position - transform.position;
            transform.up = direction;

            if (hand.position.x > transform.position.x + 0.1f)
                rubikCube.Rotate(new Vector3(0, 0, -1) * rubikRotationSpeed * Time.deltaTime, Space.World);
            if (hand.position.x < transform.position.x - 0.1f)
                rubikCube.Rotate(new Vector3(0, 0, 1) * rubikRotationSpeed * Time.deltaTime, Space.World);


            if (hand.position.z > transform.position.z + 0.1f)
                rubikCube.Rotate(new Vector3(-1, 0, 0) * rubikRotationSpeed * Time.deltaTime, Space.World);
            if (hand.position.z < transform.position.z - 0.1f)
                rubikCube.Rotate(new Vector3(1, 0, 0) * rubikRotationSpeed * Time.deltaTime, Space.World);

            //float angle = Vector3.Angle(Vector3.up, direction);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Right Hand") && !grabbingL && grabbingR)
            handR = other.gameObject.transform;

        if(other.CompareTag("Left Hand") && !grabbingR && grabbingL)
            handL = other.gameObject.transform;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Right Hand") && !grabbingL && grabbingR)
            handR = other.gameObject.transform;

        if (other.CompareTag("Left Hand") && !grabbingR && grabbingL)
            handL = other.gameObject.transform;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Right Hand"))
            handR = null;

        if (other.CompareTag("Left Hand"))
            handL = null;
    }

    private void releasedGrabR(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            grabbingR = false;
            handR = null;
        }
    }

    private void onGrabbingR(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            grabbingL = false;
            grabbingR = true;
        }
    }

    private void releasedGrabL(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            grabbingL = false;
            handL = null;
        }
    }

    private void onGrabbingL(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            grabbingR = false;
            grabbingL = true;
        }
    }

}
