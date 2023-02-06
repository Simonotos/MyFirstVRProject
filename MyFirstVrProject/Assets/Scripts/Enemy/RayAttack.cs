using UnityEngine;

public class RayAttack : MonoBehaviour
{
    [SerializeField]
    private LineRenderer rayDraw;

    [SerializeField]
    private float maxDistance, increaseSpeed;

    [SerializeField]
    private int secondsToDesappear;
    [SerializeField]
    private float secondsToCooldown;

    [SerializeField]
    private LaserRobotBehavour robot;

    [SerializeField]
    private AudioSource myAudioSource;

    [SerializeField]
    private LayerMask targettableLayers;

    private Ray ray;
    private float distance, despawningTime, cooldownTime;
    private int pos;
    public bool isDespawningAttack, inAttackRange, isCoolingDown, isAttacking;
    private bool playOneShot;
    private bool collided;

    [SerializeField]
    private Transform playerHead;

    private void Awake()
    { 
        distance = 0; pos = -1;
        isDespawningAttack = false;
        inAttackRange = false;
        isCoolingDown = false;
        isAttacking = false;
        playOneShot = true;
        collided = false;
    }

    private void Update()
    {
        if (inAttackRange && !isDespawningAttack && !collided)
        {
            if(!isAttacking)
                calculateTargetRay();

            if (!isCoolingDown)
            {
                increaseLaserLengthOvertime();
                detectCollision();
            }
        }

        if (isDespawningAttack)
        {
            despawnAttack();
        }

        if (isCoolingDown)
        {
            if (Time.time - cooldownTime > secondsToCooldown)
            {
                isCoolingDown = false;
            }
        }
    }

    private void calculateTargetRay()
    {
        Vector3 direction = playerHead.position - transform.position;
        direction.y -= 0.2f;
        ray = new Ray(transform.position, direction);
        rayDraw.startWidth = .1f; rayDraw.endWidth = .1f;
    }

    private void increaseLaserLengthOvertime()
    {
        isAttacking = true;
        ++pos;
        distance += 0.1f * increaseSpeed;

        if (distance < maxDistance)
        {
            rayDraw.positionCount += 1;
            rayDraw.SetPosition(pos, ray.direction * distance + ray.origin);
        }
        else
        {
            isDespawningAttack = true;
            despawningTime = Time.time;
        }

        if (playOneShot)
        {
            playOneShot = false;
            myAudioSource.Play();
        }
    }

    private void despawnAttack()
    {
        if (Time.time - despawningTime > secondsToDesappear)
        {
            isDespawningAttack = false;
            resetRayValues();
            isAttacking = false;
            cooldownTime = Time.time;
            isCoolingDown = true;
            playOneShot = true;
        }
    }

    private void resetRayValues()
    {
        rayDraw.positionCount = 0;
        pos = -1;
        distance = 0;
        collided = false;
    }

    private void resetVariables()
    {
        distance = 0; pos = -1;
        isDespawningAttack = false;
        inAttackRange = false;
        isCoolingDown = false;
        isAttacking = false;
        playOneShot = true;
        collided = false;
    }

    private void detectCollision()
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distance, targettableLayers))
        {

            if (hit.collider.gameObject.CompareTag("Tree"))
            {
                hit.collider.gameObject.GetComponent<LifeManager>().onCollisionDetect();
                onCollision();
            }
            else if (hit.collider.gameObject.CompareTag("Rock"))
            {
                hit.collider.gameObject.GetComponent<DestroyUnitCover>().onCollisionDetect();
                onCollision();
            }
        }
    }

    private void onCollision()
    {
        collided = true;
        isDespawningAttack = true;
        despawningTime = Time.time;
    }

    public void setAttack(bool inAttackRange)
    {
        this.inAttackRange = inAttackRange;

        if (!inAttackRange)
        {
            resetRayValues();
            resetVariables();
        }
        else
        {
            cooldownTime = Time.time;
            isCoolingDown = true;
        }
    }
}
