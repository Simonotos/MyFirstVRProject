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
            if (leftAttachTransform)
                attachTransform = leftAttachTransform;
            else
                attachTransform = transform;
        }
        else if (args.interactorObject.transform.CompareTag("Right Hand"))
        {
            if(rightAttachTransform)
                attachTransform = rightAttachTransform;
            else
                attachTransform = transform;
        }

        base.OnSelectEntered(args);
    }
}
