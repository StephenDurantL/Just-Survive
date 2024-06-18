using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject weaponPrefab;
    public Transform playerTransform; 
    public float attackSpeed = 1f;
    public int weaponLevel = 4; 

    private List<GameObject> activeWeapons = new List<GameObject>(); 
    private Queue<GameObject> weaponPool = new Queue<GameObject>(); 

    public Vector3 offsetRight = new Vector3(1, 0, 0); 
    public Vector3 offsetLeft = new Vector3(-1, 0, 0); 
    public Vector3 offsetUp = new Vector3(0, 1, 0); 
    public Vector3 offsetDown = new Vector3(0, -1, 0); 

    private Vector3 currentOffset; 

    void Start()
    {
        if(weaponLevel>4)
        {
            weaponLevel=4;
        }
        
        for (int i = 0; i < 10; i++) 
        {
            GameObject weapon = Instantiate(weaponPrefab);
            weapon.SetActive(false);
            weaponPool.Enqueue(weapon);
        }

        currentOffset = offsetRight; 
        StartCoroutine(StartAttack());
    }

    IEnumerator StartAttack()
    {
        while (true)
        {
            
            foreach (var weapon in activeWeapons)
            {
                weapon.SetActive(false);
                weaponPool.Enqueue(weapon); 
            }

            activeWeapons.Clear(); 

            
            float direction = PlayerMovement.GetInstance() != null ? PlayerMovement.GetInstance().GetLooking() : 1f;
            currentOffset = direction > 0 ? offsetRight : offsetLeft;

            
            GameObject weapon1 = GetWeaponFromPool();
            PositionAndFlipWeapon(weapon1, currentOffset, direction > 0 ? false : true);
            activeWeapons.Add(weapon1);

            
            if (weaponLevel > 1)
            {
                GameObject weapon2 = GetWeaponFromPool();
                Vector3 offset2 = direction > 0 ? offsetLeft : offsetRight;
                PositionAndFlipWeapon(weapon2, offset2, direction > 0 ? true : false);
                activeWeapons.Add(weapon2);
            }

            
            if (weaponLevel > 2)
            {
                GameObject weapon3 = GetWeaponFromPool();
                PositionAndFlipWeapon(weapon3, offsetUp, false);
                activeWeapons.Add(weapon3);
            }

            
            if (weaponLevel > 3)
            {
                GameObject weapon4 = GetWeaponFromPool();
                PositionAndFlipWeapon(weapon4, offsetDown, false);
                activeWeapons.Add(weapon4);
            }

            
            yield return new WaitForSeconds(0.1f);

            
            foreach (var weapon in activeWeapons)
            {
                weapon.SetActive(false);
                weaponPool.Enqueue(weapon);
            }

            activeWeapons.Clear(); // 清空激活武器列表

            // 等待下一次攻击
            yield return new WaitForSeconds(attackSpeed*Player.GetInstance().GetAttackSpeed());
        }
    }

    
    private GameObject GetWeaponFromPool()
    {
        if (weaponPool.Count > 0)
        {
            GameObject weapon = weaponPool.Dequeue();
            weapon.SetActive(true);
            return weapon;
        }
        else
        {
            GameObject weapon = Instantiate(weaponPrefab);
            return weapon;
        }
    }

    
    private void PositionAndFlipWeapon(GameObject weapon, Vector3 offset, bool flip)
    {
        if (playerTransform != null)
        {
            
            weapon.transform.position = playerTransform.position + offset;

            
            SpriteRenderer weaponSpriteRenderer = weapon.GetComponent<SpriteRenderer>();
            if (weaponSpriteRenderer != null)
            {
                weaponSpriteRenderer.flipX = flip;
            }
            else
            {
                
                Vector3 weaponScale = weapon.transform.localScale;
                weaponScale.x = Mathf.Abs(weaponScale.x) * (flip ? -1 : 1);
                weapon.transform.localScale = weaponScale;
            }
        }
    }

    
    public void UpgradeWeaponLevel()
    {
        weaponLevel++;
    }
}
