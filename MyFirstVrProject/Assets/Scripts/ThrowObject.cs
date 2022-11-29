using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    [SerializeField]
    private float throwForce;
    List<Vector3> objectPositions;
    private bool isHeld;

    private void Awake()
    {
        objectPositions = new List<Vector3>();
        isHeld = false;
    }

    private void Update()
    {
        if (isHeld)
        {
            if (objectPositions.Count > 15)
                objectPositions.RemoveAt(0);

            objectPositions.Add(transform.position);
        }
    }

    public void onGrab()
    {
        objectPositions.Clear();
        isHeld = true;
    }

    public void endGrab()
    {
        isHeld = false;
        throwObject();
    }

    public void throwObject() 
    {
        Vector3 direction = objectPositions[objectPositions.Count - 1] - objectPositions[0];
        GetComponent<Rigidbody>().AddForce(direction * throwForce, ForceMode.Impulse);
    }


}
