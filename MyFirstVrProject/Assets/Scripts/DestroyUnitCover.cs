using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyUnitCover : MonoBehaviour
{
    private int health = 3;

    [SerializeField]
    private Material medium, low;

    private MeshRenderer mesh;

    // Start is called before the first frame update
    void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Bullet"))
        {
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            health--;

            switch (health)
            {
                case 2:
                    mesh.material = medium;
                    break;

                case 1:
                    mesh.material = low;
                    break;

                case 0:
                    Destroy(this.gameObject);
                    break;
            }
        }
    }
}
