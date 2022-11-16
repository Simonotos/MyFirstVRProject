using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling instance;

    [SerializeField]
    private GameObject objectToPool;
    [SerializeField]
    private int amount;

    private List<GameObject> objectPool;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        objectPool = new List<GameObject>();
        GameObject tmp;

        for (int i = 0; i < amount; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            objectPool.Add(tmp); 
        }
    }

    public GameObject getPooledObject()
    {
        for (int i = 0; i < objectPool.Count; i++)
        {
            if (!objectPool[i].activeInHierarchy)
                return objectPool[i];
        }

        return null;
    }


}
