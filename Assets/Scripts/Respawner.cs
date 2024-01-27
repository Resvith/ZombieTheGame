using UnityEngine;
using System.Collections;

public class Respawner : MonoBehaviour
{
    public GameObject enemy;
    public float spawnRate = 2f;  
    public static int maxEnemies = 5;

    private float nextSpawnTime;
    private int currentEnemies = 0;

    private void Start()
    {
        nextSpawnTime = Time.time + spawnRate;
    }

    private void Update()
    {
        if (Time.time > nextSpawnTime && currentEnemies < maxEnemies)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate; 
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPoint = GetRandomPoint();
        GameObject newEnemy = Instantiate(enemy, spawnPoint, Quaternion.identity);
        Enemy enemyScript = newEnemy.GetComponent<Enemy>();
        enemyScript.OnEnemyKilled += OnEnemyKilled;
        newEnemy.transform.parent = transform; 
        currentEnemies++;
    }

    private Vector3 GetRandomPoint()
    {
        Bounds bounds = GetComponent<Collider>().bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float z = Random.Range(bounds.min.z, bounds.max.z);
        Vector3 randomPoint = new Vector3(x, transform.position.y, z);
        return randomPoint;
    }

    public void OnEnemyKilled()
    {
        currentEnemies--;
    }
}