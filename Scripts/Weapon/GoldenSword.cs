using UnityEngine;
using System.Collections;

public class GoldenSword : MonoBehaviour
{
    public int attackPower = 1;
    
    public void IncreaseDamage()
    {
        attackPower++;
    }



    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.ReduceHealthPoint(attackPower+Player.GetInstance().GetAttackPower());
            }
        }
    }
}
