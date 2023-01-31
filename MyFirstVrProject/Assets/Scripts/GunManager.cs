using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GunManager : MonoBehaviour
{
    public InputActionProperty rightControllerTrigger;
    public InputActionProperty rightControllerTriggerHold;
    public InputActionProperty leftControllerTrigger;

    [SerializeField]
    private Text log;

    private Ray ray;

    [SerializeField]
    private float maxTargettableDistance;

    [SerializeField]
    private GameObject target;
    private SpriteRenderer targetSprite;

    [SerializeField]
    private Transform bullet_origin;

    [SerializeField]
    private float bullet_velocity;

    [SerializeField]
    private LayerMask rayLayer;
    private Color newCol;

    private AudioSource myAudioSource;
    [SerializeField]
    private AudioClip chargeShotClip, releasedShotClip;

    [SerializeField]
    private GrabHandPose grabHand;

    [SerializeField]
    private HUIDLoaderGun loaderGun;

    private bool can_shoot = false;

    [SerializeField]
    private ObjectPooling bulletPool;

    [SerializeField]
    private LineRenderer rayDraw;
    private bool outOfRange;

    private GameObject currentBullet;
    private bool isHoldingTrigger;
    private bool isShooting;


    // Start is called before the first frame update
    void Awake()
    {
        targetSprite = target.GetComponent<SpriteRenderer>();
        myAudioSource = GetComponent<AudioSource>();
        ColorUtility.TryParseHtmlString("#FF9F00", out newCol);
        outOfRange = true;
        rightControllerTriggerHold.action.performed += holdingTrigger;
        rightControllerTriggerHold.action.canceled += releasedHoldingTrigger;
    }

    // Update is called once per frame
    void Update()
    {
        float rightTrigger = rightControllerTrigger.action.ReadValue<float>();
        float leftTrigger = leftControllerTrigger.action.ReadValue<float>();

        if (Input.GetKeyDown(KeyCode.Space))
            spawnBullet();

        if (grabHand.handHoldingObject != 0)
        {
            calculateRay();

            if (isHoldingTrigger == false && !isShooting)
            {
                can_shoot = true;
                //loaderGun.bulletReloadedUI();
            }
            else { can_shoot = false; }

            //Input
            if (rightTrigger > 0.5f && grabHand.handHoldingObject == 1 && can_shoot)
            {
                isShooting = true;
                spawnBullet();
            }
            else if (leftTrigger > 0.5f && grabHand.handHoldingObject == -1 && can_shoot)
            {
                isShooting = true;
                spawnBullet();
            }
        }
    }
 
    void calculateRay()
    {
        RaycastHit hit;
        ray = new Ray(bullet_origin.transform.position, bullet_origin.transform.forward);

        if (Physics.Raycast(ray, out hit, maxTargettableDistance, rayLayer))
        {
            outOfRange = false;
            targetSprite.color = newCol;
            target.transform.position = ray.GetPoint(hit.distance - 0.1f);
        }
        else
        {
            outOfRange = true;
            targetSprite.color = Color.white;
            target.transform.position = ray.GetPoint(maxTargettableDistance);
        }

        /*For visible ray*/
        /*rayDraw.SetPosition(0, bullet_origin.transform.position);
        rayDraw.SetPosition(1, bullet_origin.transform.right * 10 + bullet_origin.transform.position);*/
    }

    void spawnBullet()
    {
        /*if (!loaderGun.gunNoAmmo)
        {
            //loaderGun.justShootUI();*/
            //myAudioSource.Play();

        currentBullet = bulletPool.getPooledObject();

        if (currentBullet != null)
        {
            currentBullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
            currentBullet.transform.position = bullet_origin.transform.position;
            currentBullet.transform.SetParent(transform, true);
            currentBullet.SetActive(true);
            myAudioSource.clip = chargeShotClip;
            myAudioSource.Play();
        }
    }

    private void releaseBullet()
    {
        currentBullet.GetComponent<BoxCollider>().enabled = !outOfRange;
        currentBullet.transform.SetParent(null, true);
        myAudioSource.clip = releasedShotClip;
        myAudioSource.Play();
        currentBullet.GetComponent<Rigidbody>().AddForce(ray.direction * bullet_velocity, ForceMode.Impulse);
        isShooting = false;
    }

    private void holdingTrigger(InputAction.CallbackContext context)
    {
        if (context.performed)
            isHoldingTrigger = true;
    }

    private void releasedHoldingTrigger(InputAction.CallbackContext context)
    {
        if (context.canceled && isHoldingTrigger)
        {
            releaseBullet();
        }
        else
        {
            currentBullet.SetActive(false);
            can_shoot = true;
            isShooting = false;
            myAudioSource.Stop();
        }

        isHoldingTrigger = false;
    }
}
