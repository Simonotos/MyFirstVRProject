using System.Collections;
using UnityEngine;

public class EatFruit : MonoBehaviour
{
    private AudioSource myAudioSource;
    [SerializeField]
    private Transform playerHead;
    private MeshRenderer mesh;

    private void Awake()
    {
        myAudioSource = GetComponent<AudioSource>();
        mesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distanceVector = transform.position - playerHead.transform.position;
        float distance = Vector3.SqrMagnitude(distanceVector);

        if (distance <= 0.1f && mesh.enabled)
        {
            StartCoroutine(playSound());
        }
    }

    private IEnumerator playSound()
    {
        mesh.enabled = false;
        myAudioSource.Play();
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
}
