using UnityEngine;

public class EnemyAttackDetector : MonoBehaviour
{
    [SerializeField]
    private LaserRobotBehavour robot;
    [SerializeField]
    private AudioSource myAudioSource;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private float rangeDistance;
    private float saveRangeDistance;

    private void Awake()
    {
        saveRangeDistance = 0;
    }

    private void Update()
    {
        /*Vector3 vectorDistance = player.position - transform.position;
        float distance = Vector3.SqrMagnitude(vectorDistance);*/

        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= rangeDistance && saveRangeDistance > rangeDistance)
            inRange(true, distance);
        
        if(distance > rangeDistance && saveRangeDistance <= rangeDistance)
            inRange(false, distance);
    }

    private void inRange(bool value, float distance)
    {
        saveRangeDistance = distance;
        robot.inRange(value);

        if (value)
            myAudioSource.Play();
    }
}
