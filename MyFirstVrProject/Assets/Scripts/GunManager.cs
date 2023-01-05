using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GunManager : MonoBehaviour
{
    public InputActionProperty rightControllerTrigger;
    public InputActionProperty leftControllerTrigger;

    [SerializeField]
    private Text log;

    private Ray ray;

    [SerializeField]
    private float maxTargettableDistance;

    [SerializeField]
    private GameObject target;
    [SerializeField]
    private SpriteRenderer targetSprite;

    [SerializeField]
    private Transform bullet_origin;

    [SerializeField]
    private float bullet_velocity;
    private float delay_shooting = 0.5f;
    private float next_shot = 0f;

    [SerializeField]
    private LayerMask rayLayer;
    private Color newCol;

    [SerializeField]
    private AudioSource myAudioSource;
    [SerializeField]
    private AudioClip gunNoAmmoClip;

    [SerializeField]
    private GrabHandPose grabHand;

    [SerializeField]
    private HUIDLoaderGun loaderGun;

    private bool can_shoot = false;

    [SerializeField]
    private Animator myAnimator;

    [SerializeField]
    private ObjectPooling bulletPool;

    [SerializeField]
    private LineRenderer rayDraw;
    private bool outOfRange;


    // Start is called before the first frame update
    void Awake()
    {
        ColorUtility.TryParseHtmlString("#FF9F00", out newCol);
        outOfRange = true;
    }

    // Update is called once per frame
    void Update()
    {
        float rightTrigger = rightControllerTrigger.action.ReadValue<float>();
        float leftTrigger = leftControllerTrigger.action.ReadValue<float>();

        if (Input.GetKeyDown(KeyCode.Space))
            shootBullet(ray.direction, outOfRange);

        if (grabHand.handHoldingObject != 0)
        {
            calculateRay();

            if (Time.time > next_shot)
            {
                can_shoot = true;
                loaderGun.bulletReloadedUI();
            }
            else { can_shoot = false; }

            //Input
            if (rightTrigger > 0.5f && grabHand.handHoldingObject == 1 && can_shoot)
            {
                next_shot = Time.time + delay_shooting;
                shootBullet(ray.direction, outOfRange);
            }
            else if (leftTrigger > 0.5f && grabHand.handHoldingObject == -1 && can_shoot)
            {
                next_shot = Time.time + delay_shooting;
                shootBullet(ray.direction, outOfRange);
            }
        }
    }

    void calculateRay()
    {
        RaycastHit hit;
        ray = new Ray(bullet_origin.transform.position, bullet_origin.transform.right);

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

    void shootBullet(Vector3 direction, bool range)
    {
        if (!loaderGun.gunNoAmmo)
        {
            myAudioSource.Play();
            myAnimator.SetTrigger("shoot");
            loaderGun.justShootUI();

            GameObject bullet = bulletPool.getPooledObject();

            if (bullet != null)
            {
                bullet.GetComponent<BoxCollider>().enabled = !range;
                bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
                bullet.transform.position = bullet_origin.transform.position;
                bullet.transform.rotation = bullet_origin.transform.rotation;
                bullet.SetActive(true);
                bullet.GetComponent<Rigidbody>().AddForce(direction * bullet_velocity, ForceMode.Impulse);
                StartCoroutine(despawnBullet(bullet));
            }
        }
        else
        {
            myAudioSource.clip = gunNoAmmoClip;
            myAudioSource.Play();
        }
    }

    IEnumerator despawnBullet(GameObject bullet)
    {
        yield return new WaitForSeconds(3);

        if (bullet)
            bullet.GetComponent<BoxCollider>().enabled = false;
    }
}
