using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Pull : MonoBehaviour
{
    public InputActionReference gripPressed;

    [SerializeField]
    private GameObject playerBody;
    [SerializeField]
    private PlayerRigidbodyClimbing playerRigidbody;

    private Vector3 prevPos;
    public bool canGrab, canRecover;
    public bool onGrabbing;

    private SphereCollider handCollider;

    //Stamina
    [SerializeField]
    private Image staminaBar;
    [SerializeField]
    private float staminaReduceVelocity;
    public bool onTiring, onRecovering;
    //

    [SerializeField]
    private bool isRightHand, isLeftHand;

    public Text log;
    private int tempKnobID;
    private bool handFreezed;

    [SerializeField]
    private AudioClip grabClip1, grabClip2;
    private AudioSource myAudioSource;

    private void Awake()
    {
        handCollider = GetComponent<SphereCollider>();
        myAudioSource = GetComponent<AudioSource>();
        onGrabbing = false; canRecover = false; canGrab = false;
        gripPressed.action.started += onGrab;
        gripPressed.action.canceled += onEndGrab;
        prevPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {

        if (staminaBar.fillAmount == 0)
        {
            handCollider.enabled = false;
            onGrabbing = false;
            onTiring = false;
            onRecovering = false;
        }
        else
            handCollider.enabled = true;

        if (staminaBar.fillAmount >= 0.5f)
            changeBreathingSound(false);
        else
            changeBreathingSound(true);

        if (onGrabbing)
        {
            //freeze left/right hand
            if ((KnobID.leftHandKnobID != KnobID.rightHandKnobID && KnobID.leftHandKnobID != 0 && KnobID.rightHandKnobID != 0)
                || (isLeftHand && KnobID.leftHandKnobID == KnobID.rightHandKnobID && KnobID.leftHandKnobID != 0))
            {
                handFreezed = true;
            }
            else
                handFreezed = false;

            if (!handFreezed)
            { 
                Vector3 newPos = playerBody.transform.position + (prevPos - transform.localPosition);
                playerBody.transform.position = newPos;
            }

            if (canRecover)
            {
                onRecovering = true;
                onTiring = false;
            }
            else
            {
                onRecovering = false;
                onTiring = true;
            }
        }
        else
        {
            if (playerRigidbody.isGrounded)
                onRecovering = true;
            else
                onRecovering = false;

            onTiring = false;
        }

        prevPos = transform.localPosition;

        if (onRecovering)
            rechargeStamina();

        if (onTiring)
            reduceStamina();
    }

    private void reduceStamina()
    {
        if (staminaBar.fillAmount > 0)
            staminaBar.fillAmount -= staminaReduceVelocity * 0.1f * Time.deltaTime;
        else
            staminaBar.fillAmount = 0;
    }

    private void rechargeStamina()
    {
        if (staminaBar.fillAmount < 1)
            staminaBar.fillAmount += staminaReduceVelocity * 0.1f * Time.deltaTime;
        else
            staminaBar.fillAmount = 1;
    }

    private void onGrab(InputAction.CallbackContext context)
    {
        if (context.started && canGrab)
        {
            playRandomizeGrabbingSound();
            onGrabbing = true;
            setKnobID(tempKnobID);
        }
    }

    private void onEndGrab(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            onGrabbing = false;
            canRecover = false;
            setKnobID(0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (staminaBar.fillAmount > 0)
        {
            if (other.CompareTag("Knob"))
            {
                canGrab = true;
                tempKnobID = other.gameObject.GetInstanceID();
            }

            if (other.CompareTag("RecoverKnob"))
            {
                canGrab = true;
                canRecover = true;
                tempKnobID = other.gameObject.GetInstanceID();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (staminaBar.fillAmount > 0)
        {
            if (other.CompareTag("Knob"))
                canGrab = true;

            if (other.CompareTag("RecoverKnob"))
            {
                canGrab = true;
                canRecover = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Knob") || other.CompareTag("RecoverKnob"))
        {
            canGrab = false;
            onGrabbing = false;
            setKnobID(0);
        }
    }

    private void setKnobID(int value)
    {
        if(value == 0)
            tempKnobID = 0;

        if(isRightHand)
            KnobID.rightHandKnobID = value;
        else
            KnobID.leftHandKnobID = value;
    }

    private void playRandomizeGrabbingSound()
    {
        //randomize clip
        int randomize = Random.Range(0, 1);

        if (randomize == 0)//first clip
            myAudioSource.clip = grabClip1;
        else //second clip
            myAudioSource.clip = grabClip2;

        myAudioSource.Play();
    }

    private void changeBreathingSound(bool value)
    {
        if(isLeftHand)
            playerRigidbody.breathingFaster(value, -1);
        else
            playerRigidbody.breathingFaster(value, 1);
    }
}
