using UnityEngine;

public class BringUpPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            other.transform.position = transform.position;
    }
}
