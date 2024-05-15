using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;


    [SerializeField] bool isSpawning = false;
    [SerializeField] float spawnDelay = 1;
    float spawnTimer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > spawnTimer && isSpawning)
        {
            spawnTimer = Time.time + spawnDelay;
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        //GameObject enemy = Instantiate(enemyPrefab, GameController.Instance.sack);
        GameObject enemy = ObjectPooler.Instance.SpawnFromPool("Enemy", GameController.Instance.sack.position, Quaternion.identity);
    }
}
