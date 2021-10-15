using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject pooledObject;
    public List<GameObject> inactivePool;
    public List<GameObject> activePool;

    public int poolSize;

    // Start is called before the first frame update
    void Start()
    {
        inactivePool = new List<GameObject>();
        GameObject temp;

        for (int i = 0; i < poolSize; i++)
        {
            temp = Instantiate(pooledObject);

            inactivePool.Add(temp);
            temp.transform.parent = this.transform;
            temp.name = i.ToString();
            temp.SetActive(true);
        }
    }

    public void stash(GameObject objectToPool)
    {
        objectToPool.SetActive(false);
        inactivePool.Add(objectToPool);
        activePool.Remove(objectToPool);

        StartCoroutine(retrieve(objectToPool));
    }

    IEnumerator retrieve(GameObject objectFromPool)
    {
        yield return new WaitForSeconds(5);

        objectFromPool.SetActive(true);
        inactivePool.Remove(objectFromPool);
        activePool.Add(objectFromPool);

        objectFromPool.GetComponent<Fish>().respawn();
    }
}
