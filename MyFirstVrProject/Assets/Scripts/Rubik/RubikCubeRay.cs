using UnityEngine;

public class RubikCubeRay : MonoBehaviour
{
    /*[SerializeField]
    private LineRenderer lr;*/
    public Ray ray;

    [SerializeField]
    private int x, y, z;
    private Vector3 direction;

    private void check()
    {
        if (x == -1)
            direction = -transform.right;
        if (x == 1)
            direction = transform.right;

        if (y == -1)
            direction = -transform.up;
        if (y == 1)
            direction = transform.up;

        if (z == 1)
            direction = transform.forward;
        if (z == -1)
            direction = -transform.forward;
    }

    private void Update()
    {
        drawray();
    }

    private void drawray()
    {
        check();
        ray = new Ray(transform.position, direction);
        /*lr.startColor = Color.red; lr.endColor = Color.red;
        lr.startWidth = 0.005f; lr.endWidth = 0.005f;
        lr.SetPosition(0, ray.origin);
        lr.SetPosition(1, ray.direction + ray.origin);*/
    }
}
