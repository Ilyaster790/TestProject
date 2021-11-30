using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public GameObject prefab;

    public int size;

    public Queue<GameObject> _poolObject;

    public static ObjectPooler InstancePool;

    public void Awake()
    {
        InstancePool = this;
    }

    private void Start()
    {
        _poolObject = new Queue<GameObject>();

        for (int i = 0; i < size; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            _poolObject.Enqueue(obj);
        }
    }

    public GameObject SpawnFromPool(Vector3 position, Quaternion rotation)
    {
        if (_poolObject.Count > 0)
        {
            GameObject objectToSpwn = _poolObject.Dequeue();

            objectToSpwn.SetActive(true);
            objectToSpwn.transform.position = position;
            objectToSpwn.transform.rotation = rotation;

            return objectToSpwn;
        }
        else
            return null;
    }
}
