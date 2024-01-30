using UnityEngine;

public class Respawner : MonoBehaviour
{
    public GameObject enemy;

    [SerializeField] private int _maxEnemies = 10;
    [SerializeField] private float _spawnRate = 2f;  
    [SerializeField] private float _respawnStartTime = 0;

    private float _nextSpawnTime;
    private int _currentEnemies = 0;


    private void Start()
    {
        _nextSpawnTime = Time.time + _spawnRate;
    }

    private void Update()
    {
        if (_respawnStartTime < Time.time && Time.time > _nextSpawnTime && _currentEnemies < _maxEnemies)
        {
            SpawnEnemy();
            _nextSpawnTime = Time.time + _spawnRate; 
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPoint = GetRandomPoint();
        GameObject newEnemy = Instantiate(enemy, spawnPoint, Quaternion.identity);
        Enemy enemyScript = newEnemy.GetComponent<Enemy>();
        enemyScript.OnEnemyKilled += OnEnemyKilled;
        newEnemy.transform.parent = transform; 
        _currentEnemies++;
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
        _currentEnemies--;
    }
}