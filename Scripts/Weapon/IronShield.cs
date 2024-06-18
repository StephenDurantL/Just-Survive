using UnityEngine;

public class IronShield : MonoBehaviour
{
    public int attackPower = 10; 
    private Transform player; 

    
    public void SetPlayer(Transform playerTransform)
    {
        player = playerTransform;
    }
    public void IncreaseDamage()
    {
        attackPower++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Enemy"))
        {
            
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.ReduceHealthPoint(attackPower+Player.GetInstance().GetAttackPower());
            }
        }
    }
}
