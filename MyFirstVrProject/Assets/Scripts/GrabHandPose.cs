using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class GrabHandPose : MonoBehaviour
{
    public HandData targetRightHandPose, targetLeftHandPose;

    private Vector3 startingHandPosition, finalHandPosition;
    private Quaternion startingHandRotation, finalHandRotation;
    private Quaternion[] startingFingerRotations, finalFingerRotations;
    private Vector3[] startingFingerPositions, finalFingerPositions;

    public int handHoldingObject = 0; //1 right -1 left

    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(setupPose);
        grabInteractable.selectExited.AddListener(unsetPose);

        targetRightHandPose.gameObject.SetActive(false);
        targetLeftHandPose.gameObject.SetActive(false);
    }

    public void setupPose(BaseInteractionEventArgs arg)
    {
        if (arg.interactorObject is XRDirectInteractor)
        {
            HandData handData = arg.interactorObject.transform.GetComponentInChildren<HandData>();
            handData.animator.enabled = false;

            if (handData.handType == HandData.HandModelType.Right)
            {
                setHandDataValues(handData, targetRightHandPose);
                handHoldingObject = 1;
            }
            else if (handData.handType == HandData.HandModelType.Left)
            {
                setHandDataValues(handData, targetLeftHandPose);
                handHoldingObject = -1;
            }
       
            setHandData(handData, finalHandPosition, finalHandRotation, finalFingerRotations, finalFingerPositions);
        }
    }

    public void setHandDataValues(HandData h1, HandData h2)
    {
        startingHandPosition = h1.root.localPosition;
        finalHandPosition = h2.root.localPosition;

        startingHandRotation = h1.root.localRotation;
        finalHandRotation = h2.root.localRotation;

        startingFingerRotations = new Quaternion[h1.fingerBones.Length];
        finalFingerRotations = new Quaternion[h2.fingerBones.Length];

        startingFingerPositions = new Vector3[h1.fingerBones.Length];
        finalFingerPositions = new Vector3[h2.fingerBones.Length];

        for (int i = 0; i < h1.fingerBones.Length; i++)
        {
            startingFingerRotations[i] = h1.fingerBones[i].localRotation;
            finalFingerRotations[i] = h2.fingerBones[i].localRotation;

            startingFingerPositions[i] = h1.fingerBones[i].localPosition;
            finalFingerPositions[i] = h2.fingerBones[i].localPosition;
        }
    }

    public void setHandData(HandData h, Vector3 newPosition, Quaternion newRotation,
        Quaternion[] newBonesRotation, Vector3[] newBonesPosition)
    {
        h.root.localPosition = newPosition;
        h.root.localRotation = newRotation;

        for (int i = 0; i < newBonesRotation.Length; i++)
        {
            h.fingerBones[i].localPosition = newBonesPosition[i];
            h.fingerBones[i].localRotation = newBonesRotation[i];
        }
    }

    public void unsetPose(BaseInteractionEventArgs arg)
    {
        if (arg.interactorObject is XRDirectInteractor)
        {
            HandData handData = arg.interactorObject.transform.GetComponentInChildren<HandData>();
            handData.animator.enabled = true;
            handHoldingObject = 0;

            setHandData(handData, startingHandPosition, startingHandRotation,
                startingFingerRotations, startingFingerPositions);
        }
    }
}
