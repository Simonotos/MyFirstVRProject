using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    private Animator myAnim;
    private AudioSource myAudioSource;

    [SerializeField]
    private string typeOfBtn;

    private void Awake()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    private void onButtonPressed()
    {
        myAudioSource.Play();
        myAnim.SetTrigger("onPressed");
        RubikSelectFace.rubikSelectFaceInstance.buttonPressed(typeOfBtn);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Right Hand") || other.CompareTag("Left Hand"))
            onButtonPressed();
    }
}
