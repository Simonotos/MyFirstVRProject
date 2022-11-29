using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public GameObject objectToPool;

    [SerializeField]
    private int amount;

    private List<GameObject> objectPool;

    //components required
    [SerializeField]
    private GameObject playerHead;
    [SerializeField]
    private ActionsActivationManager actionManager;
    public ItemSO itemSO;

    [SerializeField]
    private bool compRequired;

    private int current_index, past_index;

    // Start is called before the first frame update
    void Awake()
    {
        if (compRequired)
        {
            objectToPool.GetComponent<ObjectDescriptionWindow>().playerHead = playerHead;
            objectToPool.GetComponent<GrabHandPose>().actionManager = actionManager;
        }

        objectPool = new List<GameObject>();
        GameObject tmp;

        for (int i = 0; i < amount; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            objectPool.Add(tmp);
        }

        current_index = 0;
        past_index = 0;
        
    }

    public GameObject getPooledObject()
    {
        for (int i = current_index; i < objectPool.Count; i++)
        {
            if (!objectPool[i].activeInHierarchy)
            {
                current_index = i;

                if(i == objectPool.Count / 2) //half of list
                {
                    disableHalfObjects(i);
                    past_index = i;   
                }
                else if(i == objectPool.Count - 1) //first element
                {
                    current_index = 0;
                }
                else if(i == 0 && past_index != 0)
                {
                    disableHalfObjects(objectPool.Count);
                    past_index = 0;
                }

                return objectPool[i];
            }
        }

        return null;
    }

    private void disableHalfObjects(int size)
    {
        for (int i = past_index; i < size; i++)
        {
            objectPool[i].SetActive(false);
        }
    }


}
