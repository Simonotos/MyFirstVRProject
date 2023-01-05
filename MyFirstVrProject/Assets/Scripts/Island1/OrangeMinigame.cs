using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeMinigame : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> oranges;
    private bool is_started;

    [SerializeField]
    private float delaySeconds, speed;
    private float delayTime;

    void Awake()
    {
        is_started = true;
        delayTime = 0;
    }

    void Update()
    {
        if (is_started)
        {
            if(Time.time > delayTime)
            {
                delayTime = Time.time + delaySeconds;
                throwOrange();
            }
        }
    }

    private void throwOrange()
    {
        //random number
        int r_number = 0;

        do
        {
            r_number = Random.Range(0, oranges.Count - 1);
        }
        while (!oranges[r_number]);

        oranges[r_number].GetComponent<Rigidbody>().AddForce(transform.up * speed, ForceMode.Impulse);
    }

    public void onStart()
    {
        is_started = true;
    }
}
