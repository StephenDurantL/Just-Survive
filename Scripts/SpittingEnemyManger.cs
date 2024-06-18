using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpittingEnemyManger : MonoBehaviour
{
    public GameObject enemyPrefab; 
    public Transform playerTransform; 
    public float spawnInterval = 5f; 
    public int maxEnemies = 20; 

    private List<GameObject> activeEnemies = new List<GameObject>();

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            
            if (activeEnemies.Count < maxEnemies&& !Player.GetInstance().IsDead())
            {
               
                GameObject enemy = Instantiate(enemyPrefab, GetRandomSpawnPosition(), Quaternion.identity);

                
                SpittingEnemy spittingEnemy = enemy.GetComponent<SpittingEnemy>();
                if (spittingEnemy != null)
                {
                    spittingEnemy.SetPlayer(playerTransform); 
                }

                activeEnemies.Add(enemy);
            }

            
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // 生成敌人的随机位置（可根据需要修改为适合游戏的逻辑）
    Vector3 GetRandomSpawnPosition()
    {
        float x = Random.Range(-14f, 14f);
        float y = Random.Range(-8f, 8f);
        return new Vector3(x, y, -0.1f);
    }

}
