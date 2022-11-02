using System.Collections;
using UnityEngine;
using UnityEngine.XR;
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
    private Transform bullet_origin;
    [SerializeField]
    private GameObject bullet_prefab;
    [SerializeField]
    private float bullet_velocity;
    private float delay_shooting = 0.5f;
    private float next_shot = 0f;

    [SerializeField]
    private LayerMask rayLayer;
    private Color newCol;

    private AudioSource my_audio;

    private GrabHandPose gun;


    // Start is called before the first frame update
    void Start()
    {
        my_audio = GetComponent<AudioSource>();
        ColorUtility.TryParseHtmlString("#FF9F00", out newCol);
        gun = GetComponent<GrabHandPose>();
    }


    // Update is called once per frame
    void Update()
    {
        float rightTrigger = rightControllerTrigger.action.ReadValue<float>();
        float leftTrigger = leftControllerTrigger.action.ReadValue<float>();

        if (rightTrigger > 0.5f && gun.handHoldingObject == 1)
        {
            if (Time.time > next_shot)
            {
                next_shot = Time.time + delay_shooting;
                shootBullet(ray.direction);
            }
        }
        else if(leftTrigger > 0.5f && gun.handHoldingObject == -1)
        {
            if (Time.time > next_shot)
            {
                next_shot = Time.time + delay_shooting;
                shootBullet(ray.direction);
            }
        }

        calculateRay();
    }

    void calculateRay()
    {
        RaycastHit hit;
        ray = new Ray(transform.position, transform.forward * 6);

        if(Physics.Raycast(ray, out hit, 6, rayLayer))
        {
            target.transform.position = hit.point - ray.direction * 0.1f;
            target.GetComponent<SpriteRenderer>().color = newCol;
        }
        else
        {
            target.transform.position = ray.GetPoint(6);
            target.GetComponent<SpriteRenderer>().color = Color.white;
        }

        /*For visible ray*/
        /*ray.SetPosition(0, ray_origin.transform.position);
        ray.SetPosition(1, transform.forward * 10 + ray_origin.transform.position);*/
    }

    void shootBullet(Vector3 direction)
    {
        //Play Sound
        my_audio.Play();

        GameObject bullet = Instantiate(bullet_prefab, bullet_origin.transform.position, bullet_origin.transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(direction * bullet_velocity, ForceMode.Impulse);
        StartCoroutine(despawnBullet(bullet));
    }

    IEnumerator despawnBullet(GameObject bullet)
    {
        yield return new WaitForSeconds(3);
        if(bullet)
            Destroy(bullet);
    }
}
