using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballManager : MonoBehaviour
{
    public GameObject fireballPrefab; 
    public Transform playerTransform; 
    public float detectionRadius = 10f; 
    public LayerMask enemyLayer; 
    public float fireballSpawnInterval = 2f; 
    public float fireballOffset = 1f; 
    public int weaponLevel = 4;
    private bool isPerfect = false;

    void Start()
    {
        StartCoroutine(SpawnFireballsWhenEnemiesInRange());
    }

    IEnumerator SpawnFireballsWhenEnemiesInRange()
    {
        while (true)
        {
            Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(playerTransform.position, detectionRadius, enemyLayer);

            if (enemiesInRange.Length > 0)
            {
                if(weaponLevel>4)
                {
                    isPerfect=true;
                    weaponLevel=4;
                }
                
                for (int i = 0; i < weaponLevel*2; i++)
                {
                    // 计算每个火球的生成角度
                    float angle = 360f / weaponLevel * i;
                    Vector3 offset = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad) * fireballOffset,
                                                Mathf.Sin(angle * Mathf.Deg2Rad) * fireballOffset,
                                                0);

                    // 在偏移的位置生成火球
                    GameObject fireball = Instantiate(fireballPrefab, playerTransform.position + offset, Quaternion.identity);

                    // 锁定目标敌人
                    Transform target = enemiesInRange[i % enemiesInRange.Length].transform;
                    Fireball fireballScript = fireball.GetComponent<Fireball>();
                    if (fireballScript != null)
                    {
                        fireballScript.SetTarget(target);
                    }
                    if (isPerfect)
                    {
                        fireballScript.IncreaseDamage();
                    }
                }

            }

            
            yield return new WaitForSeconds(fireballSpawnInterval*Player.GetInstance().GetAttackSpeed());
        }
    }

    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(playerTransform.position, detectionRadius);
    }

    public void UpgradeWeaponLevel()
    {
        weaponLevel++;
    }
}
