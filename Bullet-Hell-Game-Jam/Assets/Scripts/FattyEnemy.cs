using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FattyEnemy : Enemy
{
    [Header("The enemy spawned by death")]
    public GameObject enemySpawn;

    private int spawnCount;

    public EnemySpawner enemySpawner;

    // Start is called before the first frame update
    override public void Start()
    {
        spawnCount = Random.Range(3, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Explode()
    {
        for (int i = 1; i <= spawnCount; i++)
        {
            enemySpawner.InstantiateEnemy(enemySpawn, transform.position);
        }
    }
}
