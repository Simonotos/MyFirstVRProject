using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HUIDLoaderGun : MonoBehaviour
{
    [SerializeField]
    private Text loader_txt;

    [SerializeField]
    private GameObject sliderObj;
    [SerializeField]
    private Slider sliderComp;

    [SerializeField]
    private int maxAmmo;

    public InputActionReference reloadButton;
    [SerializeField]
    private float reloadingTime;
    private float initialReloadingTime;

    public bool gunNoAmmo = false;
    private int ammo = 0;
    private bool reloading = false;

    [SerializeField]
    private AudioSource gunAudioSource;
    [SerializeField]
    private AudioClip gunReloading, gunShooting;

    [SerializeField]
    private ItemSO bulletSO;
    [SerializeField]
    private InventoryController inventoryController;

    [SerializeField]
    private GrabHandPose grabHand;


    private void Awake()
    {
        sliderComp.maxValue = reloadingTime;
        initialReloadingTime = reloadingTime;
        ammo = maxAmmo;
        reloadButton.action.performed += onReloadEvent;
    }

    private void Update()
    {
        if (reloading)
            reloadingHUID();
    }

    public void justShootUI()
    {
        --ammo;

        if (ammo == 0)
        {
            gunNoAmmo = true;
            loader_txt.color = Color.red;
            loader_txt.text = "Press B to Reload";
        }
        else {loader_txt.text = "0/" + ammo.ToString();}

    }

    public void bulletReloadedUI()
    {
        if(!gunNoAmmo)
            loader_txt.text = "1/" + ammo.ToString();
    }

    public void onReloadEvent(InputAction.CallbackContext context)
    {
        if (grabHand.handHoldingObject != 0 && gunNoAmmo) { 
            if (context.performed && inventoryController.removeItem(bulletSO) != -1)
            {
                loader_txt.text = "";
                gunAudioSource.clip = gunReloading;
                gunAudioSource.Play();
                sliderObj.SetActive(true);
                reloadingTime += Time.time;
                reloading = true;
            }
            else
            {
                StartCoroutine(noAmmoAlert());
            }
        }
    }

    public void reloadingHUID()
    {
        float seconds = Time.time;

        if (seconds < reloadingTime)
            sliderComp.value += Time.deltaTime;
        else
        {
            gunAudioSource.clip = gunShooting;
            reloadingTime = initialReloadingTime;
            reloading = false;
            ammo = maxAmmo;
            gunNoAmmo = false;
            loader_txt.color = Color.black;
            sliderComp.value = 0;
            sliderObj.SetActive(false);
        }
    }

    private IEnumerator noAmmoAlert()
    {
        loader_txt.text = "No Ammo";
        yield return new WaitForSeconds(2);
        if(ammo == 0)
            loader_txt.text = "Press B to Reload";
    }

}
