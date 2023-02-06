using UnityEngine;
using UnityEngine.UI;

public class ObjectDescriptionWindow : MonoBehaviour
{
    [SerializeField]
    private ItemSO itemSO;
    [SerializeField]
    private Text descriptionText;
    [SerializeField]
    private GameObject canvasObj;
    public GameObject playerHead;

    private bool isHeld;

    private void Awake()
    {
        descriptionText.text = itemSO.Description;
        isHeld = false;
    }

    private void Update()
    {

        if (isHeld)
        {
            Vector3 distanceVector = transform.position - playerHead.transform.position;
            float distance = Vector3.SqrMagnitude(distanceVector);

            if (distance <= 0.2f)
            {
                canvasObj.SetActive(true);
            }
            else if (canvasObj.activeInHierarchy)
                canvasObj.SetActive(false);
        }
        else if (canvasObj.activeInHierarchy)
            canvasObj.SetActive(false);
    }


    public void onGrab()
    {
        isHeld = true;
    }

    public void endGrab()
    {
        isHeld = false;
    }
}
