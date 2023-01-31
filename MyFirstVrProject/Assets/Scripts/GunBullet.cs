using System.Collections;
using UnityEngine;

public class GunBullet : MonoBehaviour
{
    private float desired_size = 0.1f;
    private float current_size = 0;
    [SerializeField]
    private float increaseSpeed;

    [SerializeField]
    private BoxCollider myCollider;

    // Start is called before the first frame update
    void Awake()
    {
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (current_size < desired_size)
        {
            increaseSize();
        }
    }

    private void increaseSize()
    {
        current_size += 0.01f * increaseSpeed * Time.deltaTime;
        transform.localScale = new Vector3(current_size, current_size, current_size);
    }

    public void resetValues()
    {
        transform.localScale = Vector3.zero;
        current_size = 0;
    }

    private void OnEnable()
    {
        resetValues();
    }
}
