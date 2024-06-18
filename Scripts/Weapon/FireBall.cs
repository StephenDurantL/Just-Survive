using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 5f; // 火球移动的速度
    public int damage = 3; // 火球的伤害值
    private Transform target; // 目标敌人的 Transform
    public float lifetime = 5f;


    public void IncreaseDamage()
    {
        damage++;
    }
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject); 
            return;
        }

        
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.ReduceHealthPoint(damage+Player.GetInstance().GetAttackPower());
            }

            
            Destroy(gameObject);
        }
    }
}
