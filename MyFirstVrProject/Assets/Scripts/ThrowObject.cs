using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowObject : MonoBehaviour
{
    [SerializeField]
    private float thrownessForce;
    List<Vector3> objectPositions;

    [SerializeField]
    private Text log;

    private void Awake()
    {
        objectPositions = new List<Vector3>();
    }

    public void onGrab()
    {
        objectPositions.Clear();
        log.text = "Ho grabbato l'oggetto";
    }

    public void endGrab()
    {
        log.text = "Ho rilasciato l'oggetto";
    }


}
