using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEnemyManager : MonoBehaviour
{
    public GameObject specialEnemyPrefab; 
    public Transform playerTransform; 
    public float spawnInterval = 5f; 
    public List<Transform> spawnPoints; 

    void Start()
    {
        
        StartCoroutine(SpawnSpecialEnemies());
    }

    IEnumerator SpawnSpecialEnemies()
    {
        while (!Player.GetInstance().IsDead())
        {
            
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
            GameObject enemy = Instantiate(specialEnemyPrefab, spawnPoint.position, Quaternion.identity);

            
            SpecialEnemy specialEnemyScript = enemy.GetComponent<SpecialEnemy>();
            if (specialEnemyScript != null)
            {
                specialEnemyScript.playerTransform = playerTransform;
            }

            
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
