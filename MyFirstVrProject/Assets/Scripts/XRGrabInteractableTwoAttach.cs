using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRGrabInteractableTwoAttach : XRGrabInteractable
{
    [SerializeField]
    private Transform rightAttachTransform, leftAttachTransform;
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactorObject.transform.CompareTag("Left Hand"))
        {
            attachTransform = leftAttachTransform;
        }
        else if (args.interactorObject.transform.CompareTag("Right Hand"))
        {
            attachTransform = rightAttachTransform;
        }

        base.OnSelectEntered(args);
    }
}
