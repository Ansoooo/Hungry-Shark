using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public int fishType; // 0 for good , 1 for bad
    Vector3 target;

    public GameObject player;

    public float swimSpeed;

    public float maxLifeTime;
    public float minLifeTime;
    float expectedLifeTime;
    float lifeTime;

    float maxWaitTime;
    float waitTime;

    public GameObject spawnArea;
    MeshCollider spawnAreaCol;

    public GameObject good, bad;

    public int getFishType()
    {
        return fishType;
    }

    public void spawn()
    {
        this.transform.position = new Vector3(
            Random.Range(spawnArea.transform.position.x - (spawnAreaCol.bounds.size.x * 0.5f), spawnArea.transform.position.x + (spawnAreaCol.bounds.size.x * 0.5f)),
             10,
              Random.Range(spawnArea.transform.position.z - (spawnAreaCol.bounds.size.z * 0.5f), spawnArea.transform.position.z + (spawnAreaCol.bounds.size.z * 0.5f))
            );

        if ((this.transform.position - player.transform.position).magnitude < 5.0f)
            spawn(); //pick new location, too close to player
    }

    float getTarget()
    {
       target = new Vector3(
           Random.Range(spawnArea.transform.position.x - (spawnAreaCol.bounds.size.x * 0.5f), spawnArea.transform.position.x + (spawnAreaCol.bounds.size.x * 0.5f)),
            10,
             Random.Range(spawnArea.transform.position.z - (spawnAreaCol.bounds.size.z * 0.5f), spawnArea.transform.position.z + (spawnAreaCol.bounds.size.z * 0.5f))
           );

        if ((target - player.transform.position).magnitude < 5.0f)
            getTarget(); //pick  new target, too close to player;

        return 0;
    }

    void moveFish()
    {
        waitTime = waitTime > maxWaitTime ? getTarget() : waitTime; // if reached end of waitTime retrieve new target, else keep swimming towards target
        waitTime += Time.deltaTime;

        this.transform.position = Vector3.MoveTowards(this.transform.position, target, swimSpeed * Time.deltaTime);
    }

    public void respawn()
    {
        fishType = Random.Range(0, 2) == 0 ? 0 : 1;
        expectedLifeTime = Random.Range(minLifeTime, maxLifeTime);
        lifeTime = 0;
        waitTime = 0;
        spawn();
    }

    void Start()
    {
        spawnAreaCol = spawnArea.GetComponent<MeshCollider>();

        fishType = Random.Range(0, 2) == 0 ? 0 : 1;

        expectedLifeTime = Random.Range(minLifeTime, maxLifeTime);

        maxWaitTime = 1;
        waitTime = 0;
    }

    void Update()
    {
        moveFish();

        if (lifeTime > expectedLifeTime)
        {
            respawn();
        }
        else
            lifeTime += Time.deltaTime;

        if(fishType == 0)
        {
                good.SetActive(true);
                bad.SetActive(false);
        }
        else if (fishType == 1)
        {
            good.SetActive(false);
            bad.SetActive(true);
        }
    }
}
