using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f; 
    public int damage = 10; 



    private Vector2 direction; 
    public float lifetime = 5f;

    
    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection;
    }
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        
        transform.Translate(direction * speed * Time.deltaTime);
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 检查碰撞对象是否是玩家
        if (collision.CompareTag("Player"))
        {
            
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.ReduceHealthPoint(damage); 
            }

            
            Destroy(gameObject);
        }
    }

}
