using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
using UnityEngine.XR;

public class LightsaberManager : MonoBehaviour
{
    private float fullHeight;
    public bool go_up = false;
    public bool go_down = false;

    [SerializeField]
    private XRController rightController;
    public InputHelpers.Button activationButton; 
    private float activationThreshold = 0.1f;
    [SerializeField]
    private float velocity = 10f;

    private AudioSource myAudioSource;
    [SerializeField]
    private AudioClip on, off;

    [SerializeField]
    private Text log;
    private int open_close = -1; //0 : open 1 : close
    private bool button_locked = false;

    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        fullHeight = transform.localScale.y;
        transform.localScale = new Vector3(transform.localScale.x, 0, transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (!button_locked)
        {
            rightController.inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool pressed);

            if (pressed)
            {
                button_locked = true;
                ++open_close;

                switch (open_close)
                {
                    case 0:
                        go_up = true;
                        myAudioSource.clip = on;
                        myAudioSource.Play();
                    break;

                    case 1:
                        go_down = true;
                        myAudioSource.clip = off;
                        myAudioSource.Play();
                    break;
                }
                this.log.text += "premuto";
            }
        }
    }

    void FixedUpdate()
    {
        if (go_up && !go_down)
            startAnimationUp();
        else if (go_down && !go_up)
            startAnimationDown();
    }

    void startAnimationUp(){
        if (transform.localScale.y < fullHeight)
            transform.localScale += new Vector3(0, 0.01f, 0) * velocity * Time.deltaTime;
        else
        {
            go_up = false;
            button_locked = false;
        }
    }

    void startAnimationDown(){
        if(transform.localScale.y > 0)
            transform.localScale -= new Vector3(0, 0.01f, 0) * velocity * Time.deltaTime;
        else
        {
            open_close = -1;
            go_down = false;
            button_locked = false;
        }
    }
}
