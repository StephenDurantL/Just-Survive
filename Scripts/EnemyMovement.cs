using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Transform playerTransform;
    Enemy character;
    SpriteRenderer spriteRenderer;
    
    private Vector2 moveInput;
    private bool isPlayerDead;

    void Awake()
    {
        character = GetComponent<Enemy>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        moveInput = new Vector2();
        isPlayerDead = false; 
    }

    void Update()
    {
        
        isPlayerDead = Player.GetInstance().IsDead(); 

        
        if (isPlayerDead) return;

        
        moveInput = (playerTransform.position - transform.position).normalized;

        transform.position += (Vector3)(moveInput * character.GetSpeed() * Time.deltaTime);

        
        if (moveInput.x < 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveInput.x > 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    public Vector2 GetDirection()
    {
        return moveInput;
    }
}
