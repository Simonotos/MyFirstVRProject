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
    public bool isDespawningAttack, inAttackRange, isCoolingDown, isAttacking, canDetectCollision;
    private bool playOneShot;

    private void Awake()
    { 
        distance = 0; pos = -1;
        isDespawningAttack = false;
        inAttackRange = false;
        isCoolingDown = false;
        isAttacking = false;
        canDetectCollision = true;
        playOneShot = true;
    }

    private void Update()
    {
        if (inAttackRange && !isDespawningAttack)
        {
            calculateTargetRay();

            if(!isCoolingDown)
                increaseLaserLengthOvertime();
        }

        if (isDespawningAttack)
        {
            if (canDetectCollision)
            {
                canDetectCollision = false;
                detectCollision();
            }

            despawnAttack();
        }

        if (isCoolingDown)
        {
            if (Time.time - cooldownTime > secondsToCooldown)
            {
                canDetectCollision = true;
                isCoolingDown = false;
            }
        }
    }

    private void calculateTargetRay()
    {
        ray = new Ray(transform.position, transform.up);
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
    }

    private void resetVariables()
    {
        distance = 0; pos = -1;
        isDespawningAttack = false;
        inAttackRange = false;
        isCoolingDown = false;
        isAttacking = false;
        canDetectCollision = true;
        playOneShot = true;
    }

    private void detectCollision()
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDistance, targettableLayers))
        {
            Debug.Log("Eccomi");
            hit.collider.gameObject.GetComponent<LifeManager>().onCollisionDetect();
        }
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
