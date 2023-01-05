using UnityEngine;

public class LaserRobotBehavour : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private float rotationSpeed;
    private bool isInsideRange = false;

    [SerializeField]
    private RayAttack rayAttack;

    [SerializeField]
    private RobotLifeManager robotHealth;

    [SerializeField]
    private AudioSource myAudioSource;

    // Update is called once per frame
    void Update()
    {
        if (!rayAttack.isAttacking || rayAttack.isCoolingDown)
        {
            if (!isInsideRange)
                rotateAroundItself();
            else
                rotateToPlayer();
        }
    }

    private void rotateToPlayer()
    {
        var newRotation = Quaternion.LookRotation(player.position - transform.position, Vector3.up);
        newRotation.x = 0;
        newRotation.z = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 100 * Time.deltaTime);
    }

    private void rotateAroundItself()
    {
        transform.RotateAround(transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
    }

    public void inRange(bool isInsideRange)
    {
        this.isInsideRange = isInsideRange;
        robotHealth.activateCanvas(isInsideRange);
        rayAttack.setAttack(isInsideRange);
        myAudioSource.enabled = !isInsideRange;
    }
}
