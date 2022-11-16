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


    // Start is called before the first frame update
    void Awake()
    {
        ColorUtility.TryParseHtmlString("#FF9F00", out newCol);
    }

    // Update is called once per frame
    void Update()
    {
        calculateRay();

        if (Input.GetKeyDown(KeyCode.Space))
            shootBullet(ray.direction);

        if (grabHand.handHoldingObject != 0)
        {
            float rightTrigger = rightControllerTrigger.action.ReadValue<float>();
            float leftTrigger = leftControllerTrigger.action.ReadValue<float>();

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
                shootBullet(ray.direction);
            }
            else if (leftTrigger > 0.5f && grabHand.handHoldingObject == -1 && can_shoot)
            {
                next_shot = Time.time + delay_shooting;
                shootBullet(ray.direction);
            }
        }
    }

    void calculateRay()
    {
        RaycastHit hit;
        ray = new Ray(transform.position, transform.forward * 6);

        if(Physics.Raycast(ray, out hit, 6, rayLayer))
        {
            Vector3 direction = hit.point - ray.direction * 0.1f;
            target.transform.position = new Vector3(direction.x, target.transform.position.y, direction.z);
            targetSprite.color = newCol;
        }
        else
        {
            target.transform.position = ray.GetPoint(6);
            targetSprite.color = Color.white;
        }

        /*For visible ray*/
        /*ray.SetPosition(0, ray_origin.transform.position);
        ray.SetPosition(1, transform.forward * 10 + ray_origin.transform.position);*/
    }

    void shootBullet(Vector3 direction)
    {
        if (!loaderGun.gunNoAmmo)
        {
            myAudioSource.Play();
            myAnimator.SetTrigger("shoot");
            loaderGun.justShootUI();

            GameObject bullet = ObjectPooling.instance.getPooledObject();
            bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;

            if (bullet != null)
            {
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

        if(bullet)
            bullet.SetActive(false);
    }
}
