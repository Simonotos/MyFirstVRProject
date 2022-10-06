using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;

public class HandsIdentity : MonoBehaviour
{
    public Text log;
    private InputDevice targetDevice;

    public InputDeviceCharacteristics controllerType;
    public GameObject handModelPrefab;
    private GameObject handModelSpawned;

    private Animator handAnimator;

    private bool holding_gun = false;

    // Start is called before the first frame update
    void Start()
    {
        InitializeDevice();
    }

    // Update is called once per frame
    void Update()
    {
        if(!targetDevice.isValid)
            InitializeDevice();

        if(handModelSpawned){
            this.handModelSpawned.SetActive(true);
            updateHandAnimation();
        }
    }

    void updateHandAnimation(){
        if(targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float trigger_value)){
            handAnimator.SetFloat("Trigger", trigger_value);
        }
        else{
            handAnimator.SetFloat("Trigger", 0);
        }

        if(targetDevice.TryGetFeatureValue(CommonUsages.grip, out float grip_value)){
            handAnimator.SetFloat("Grip", grip_value);

            if (holding_gun && grip_value == 1)
                handAnimator.SetBool("holdingGun", true);
            else if(holding_gun && grip_value == 0)
            {
                holding_gun = false;
                handAnimator.SetBool("holdingGun", false);
            }
        }
        else{
            handAnimator.SetFloat("Grip", 0);
        }
    }

    void InitializeDevice(){
        List<InputDevice> devices = new List<InputDevice>();

        foreach (var item in devices)
        {
            //log.text += item;
        }

        InputDevices.GetDevicesWithCharacteristics(controllerType, devices);

        if(devices.Count > 0){
            targetDevice = devices[0];

            //hand like child of this gameobject
            this.handModelSpawned = Instantiate(this.handModelPrefab, transform);
            this.handAnimator = this.handModelSpawned.GetComponent<Animator>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Gun"))
        {
            Debug.Log(" Hai preso la pistola");
            holding_gun = true;
        }
    }

}
