using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    private Animator myAnim;

    [SerializeField]
    private string typeOfBtn;

    private void onButtonPressed()
    {
        myAnim.SetTrigger("onPressed");
        RubikSelectFace.rubikSelectFaceInstance.buttonPressed(typeOfBtn);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Right Hand") || other.CompareTag("Left Hand"))
            onButtonPressed();
    }
}
