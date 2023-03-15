using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] public GameObject objPool = default;
    private List<GameObject> objPools = default;
    private int poolSize = default;

    private void Awake()
    {
        objPools = new List<GameObject>();

        poolSize = 30;
    }

    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(objPool);
            obj.SetActive(false);
            objPools.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < objPools.Count; i++)
        {
            if (!objPools[i].activeInHierarchy)
            {
                return objPools[i];
            }
        }

        GameObject obj = Instantiate(objPool);
        obj.SetActive(false);
        objPools.Add(obj);

        return obj;
    }

    public void ReturnPooledObject(GameObject obj)
    {
        obj.SetActive(false);
    }

}
