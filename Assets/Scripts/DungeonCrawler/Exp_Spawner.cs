using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Exp_Spawner : MonoBehaviour
{
    public static Exp_Spawner Instance;
    [SerializeField] GameObject expPrefab;
    [SerializeField] Transform[] spots;

    public bool ShouldSpawn = true;
    [SerializeField] float spawnDelay = 5f;
    float spawnTimer;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ObjectPooler.Instance.SpawnFromPool("exp", spots[Random.Range(0, spots.Length)].position, Quaternion.identity);
        //Instantiate(expPrefab, spots[Random.Range(0, spots.Length)].position, Quaternion.identity, GameController.Instance.sack);
        ShouldSpawn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (ShouldSpawn)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer > spawnDelay)
            {
                spawnTimer = 0;
                ObjectPooler.Instance.SpawnFromPool("exp", spots[Random.Range(0, spots.Length)].position, Quaternion.identity);
                //Instantiate(expPrefab, spots[Random.Range(0, spots.Length)].position, Quaternion.identity, GameController.Instance.sack);
                ShouldSpawn = false;
            }
        }
    }
}
