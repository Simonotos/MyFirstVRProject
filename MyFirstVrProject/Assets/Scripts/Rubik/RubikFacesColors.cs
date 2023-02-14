using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RubikFacesColors : MonoBehaviour
{
    [SerializeField]
    private RubikCubeRay row1PivotRay, row3PivotRay, faceColumn1PivotRay, faceColumn3PivotRay, lateralColumn1PivotRay, lateralColumn3PivotRay;

    [SerializeField]
    private Transform centralCubePivot; //they stay the same

    [SerializeField]
    private LayerMask targettableLayers;

    [SerializeField]
    private Transform pivot;

    //whichs tell me what are the cubes that have to be moved according to face choosed
    [SerializeField]
    private RubikCubeRay[] row1Rays, row2Rays, row3Rays, faceColumn1Rays, faceColumn2Rays, faceColumn3Rays, lateralColumn1Rays, lateralColumn2Rays, lateralColumn3Rays;

    [SerializeField]
    private Transform cubesMainParent;

    private List<Transform> pastRotationCubes;

    public bool rotating = false;

    private void Awake()
    {
        pastRotationCubes = new List<Transform>();
    }

    public void readCubeFromRays(string faceToRotate, Transform cubePivot)
    {
        switch (faceToRotate)
        {

            //Rows
            case "Row1":
                foreach (var item in row1Rays)
                {
                    Ray ray = item.ray;
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 3, targettableLayers))
                    {
                        if (hit.collider)
                        {
                            pastRotationCubes.Add(hit.collider.transform);
                        }
                    }
                }
                break;
           
                

            case "Row2":
                foreach (var item in row2Rays)
                {
                    Ray ray = item.ray;
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 3, targettableLayers))
                    {
                        if (hit.collider)
                        {
                            pastRotationCubes.Add(hit.collider.transform);
                        }
                    }
                }
                break;

                
            case "Row3":
                foreach (var item in row3Rays)
                {
                    Ray ray = item.ray;
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 3, targettableLayers))
                    {
                        if (hit.collider)
                        {
                            pastRotationCubes.Add(hit.collider.transform);
                        }
                    }
                }
                break;

            //Face Column
            case "FaceColumn1":
                foreach (var item in faceColumn1Rays)
                {
                    Ray ray = item.ray;
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 3, targettableLayers))
                    {
                        if (hit.collider)
                        {
                            pastRotationCubes.Add(hit.collider.transform);
                        }
                    }
                }
                break;

        
            case "FaceColumn2":
                foreach (var item in faceColumn2Rays)
                {
                    Ray ray = item.ray;
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 3, targettableLayers))
                    {
                        if (hit.collider)
                        {
                            pastRotationCubes.Add(hit.collider.transform);
                        }
                    }
                }
                break;

            case "FaceColumn3":
                foreach (var item in faceColumn3Rays)
                {
                    Ray ray = item.ray;
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 3, targettableLayers))
                    {
                        if (hit.collider)
                        {
                            pastRotationCubes.Add(hit.collider.transform);
                        }
                    }
                }
                break;

            //Lateral Column
            case "LateralColumn1":
                foreach (var item in lateralColumn1Rays)
                {
                    Ray ray = item.ray;
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 3, targettableLayers))
                    {
                        if (hit.collider)
                        {
                            pastRotationCubes.Add(hit.collider.transform);
                        }
                    }
                }
                break;

            case "LateralColumn2":
                foreach (var item in lateralColumn2Rays)
                {
                    Ray ray = item.ray;
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 3, targettableLayers))
                    {
                        if (hit.collider)
                        {
                            pastRotationCubes.Add(hit.collider.transform);
                        }
                    }
                }
                break;

            case "LateralColumn3":
                foreach (var item in lateralColumn3Rays)
                {
                    Ray ray = item.ray;
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 3, targettableLayers))
                    {
                        if (hit.collider)
                        {
                            pastRotationCubes.Add(hit.collider.transform);
                        }
                    }
                }
                break;
        }

        //setParent
        foreach (var item in pastRotationCubes)
        {
            item.transform.SetParent(cubePivot, true);
        }
    }

    private Transform readPivotCubeFromPivotRays(string faceToRotate)
    {

        if (faceToRotate.Equals("Row1"))
        {
            Ray ray = row1PivotRay.ray;
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 3, targettableLayers))
            {
                if (hit.collider)
                {
                    return hit.collider.transform;
                }
            }
        }
        
        else if (faceToRotate.Equals("Row3"))
        {
            Ray ray = row3PivotRay.ray;
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 3, targettableLayers))
            {
                if (hit.collider)
                    return hit.collider.transform;
            }
        }
    

        else if (faceToRotate.Equals("FaceColumn1"))
        {
            Ray ray = faceColumn1PivotRay.ray;
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 3, targettableLayers))
            {
                if (hit.collider)
                    return hit.collider.transform;
            }
        }

        
        else if (faceToRotate.Equals("FaceColumn3"))
        {
            Ray ray = faceColumn3PivotRay.ray;
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 3, targettableLayers))
            {
                if (hit.collider)
                    return hit.collider.transform;
            }
        }

        else if (faceToRotate.Equals("LateralColumn1"))
        {
            Ray ray = lateralColumn1PivotRay.ray;
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 3, targettableLayers))
            {
                if (hit.collider)
                    return hit.collider.transform;
            }
        }

        else if (faceToRotate.Equals("LateralColumn3"))
        {
            Ray ray = lateralColumn3PivotRay.ray;
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 3, targettableLayers))
            {
                if (hit.collider)
                    return hit.collider.transform;
            }
        }
        
        return centralCubePivot;
    }

    private void unparentCubes()
    {
        //unparent
        if (pastRotationCubes.Count != 0)
        {
            foreach (var item in pastRotationCubes)
            {
                item.transform.SetParent(cubesMainParent, true);
            }
        }

        pastRotationCubes.Clear();
    }

    public void rotateFace(string faceToRotate, int x, int y, int z)
    {
        unparentCubes();
        Transform cubePivot = readPivotCubeFromPivotRays(faceToRotate);
        pivot.transform.localPosition = cubePivot.localPosition;
        pivot.transform.localEulerAngles = Vector3.zero;
        readCubeFromRays(faceToRotate, pivot);

        /*cooldownTime = Time.time;
        isCoolingDown = true;*/
        float degree = 90;
        pivot.Rotate(x * degree, y * degree, z * degree, Space.Self);
    }
}
