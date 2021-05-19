using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] [Range(0, 50)] int poolSize = 5;

    [SerializeField] [Range(0.1f, 30f)] float spawnTimer;
    GameObject[] pool;


    void Awake()
    {
        PopulatePool();
    }

    private void PopulatePool()
    {
        pool = new GameObject[poolSize];

        for (int i = 0; i < poolSize; i++)
        {
            //populate the pool with pre created GameObjs
            //(here the GameObjs are enemy prefabs)
            pool[i] = Instantiate(enemyPrefab, transform);

            // disable the GameObj by default
            pool[i].SetActive(false);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            // Enable an object from the pool
            EnableObjectInPool();
            //wait for time between spawns
            yield return new WaitForSeconds(spawnTimer);
        }
    }

    void EnableObjectInPool()
    {
        for (int i = 0; i < pool.Length; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                //Set an inactive object in the pool active
                pool[i].SetActive(true);
                //return so that only one object is set active when the function is called
                return;
            }
        }
    }
}
