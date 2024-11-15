using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Vector2 spawnRangeBL;
    public Vector2 spawnRangeUR;
    public float cooldown;

    private float nextSpawn;

    private void Start()
    {
        nextSpawn = Time.time;
    }

    private void Update()
    {
        if (Time.time > nextSpawn)
        {
            float spawnX = Random.Range(spawnRangeBL.x, spawnRangeUR.x);
            float spawnY = Random.Range(spawnRangeBL.y, spawnRangeUR.y);
            Instantiate(enemyPrefab, new Vector3(spawnX, spawnY, 0f), Quaternion.identity);
            nextSpawn += cooldown;
        }
    }
}
