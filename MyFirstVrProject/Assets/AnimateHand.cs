using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHand : MonoBehaviour
{
    public InputActionProperty grabAnimation;
    [SerializeField]
    private Animator my_anim;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float grabValue = grabAnimation.action.ReadValue<float>();
        my_anim.SetFloat("Grip", grabValue);
    }
}
