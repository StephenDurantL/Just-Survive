using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;  
    public float spawnRate = 2.0f;     
    private float nextSpawnTime = 0.0f;

    void Update()
    {
        if (Time.time >= nextSpawnTime && !Player.GetInstance().IsDead())
        {
            
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefabs.Length == 0)
        {
            
            return;
        }

        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        Vector3 spawnPosition = GetRandomPosition();
        

        Instantiate(enemyPrefabs[enemyIndex], spawnPosition, Quaternion.identity);
    }

    Vector3 GetRandomPosition()
    {
        
        float x = Random.Range(-14.0f, 14.0f);
        float y = Random.Range(-8.0f, 8.0f);
        return new Vector3(x, y, -0.15f);  
    }
}
