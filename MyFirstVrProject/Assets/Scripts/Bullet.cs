using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 direction;

    [SerializeField]
    private float bullet_velocity;
    private Rigidbody rgb;
    private bool can_shoot = true;

    // Start is called before the first frame update
    void Start()
    {
        rgb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (direction != null && can_shoot)
        {
            can_shoot = false;
            rgb.AddForce(direction * bullet_velocity, ForceMode.Impulse);
            StartCoroutine(despawnBullet(this.gameObject));
        }
    }

    IEnumerator despawnBullet(GameObject bullet)
    {
        yield return new WaitForSeconds(3);
        if (bullet)
            Destroy(bullet);
    }
}
