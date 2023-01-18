using UnityEngine;

public class FakeHand : MonoBehaviour
{
    public GameObject pivot, rightFakeHandModel, leftFakeHandModel;

    public void showFakeHand(Vector3 position, Quaternion rotation, int handType)
    {
        pivot.transform.position = position;
        pivot.transform.rotation = rotation;

        if (handType == 1)
            rightFakeHandModel.SetActive(true);
        else
            leftFakeHandModel.SetActive(true);
    }

    public void hideFakeHand()
    {
        leftFakeHandModel.SetActive(false);
        rightFakeHandModel.SetActive(false);
    }
}
