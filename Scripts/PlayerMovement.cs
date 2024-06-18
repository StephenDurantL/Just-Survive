using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    static PlayerMovement instance;

    Animator animator;       
    Player character;
    private Vector2 moveInput;
    private SpriteRenderer spriteRenderer;
    private float lastDirection = 1f; 

    void Start()
    {
        character = GetComponent<Player>();
        animator = GetComponent<Animator>();  
        spriteRenderer = GetComponent<SpriteRenderer>();
        instance = this;
    }

    void Update()
    {
        
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        float actualSpeed = moveInput.magnitude * character.GetSpeed() / 10f;

        
        transform.Translate(Vector2.right * moveInput.x * character.GetSpeed() / 10f * Time.deltaTime);
        transform.Translate(Vector2.up * moveInput.y * character.GetSpeed() / 10f * Time.deltaTime);

       
        animator.SetFloat("Speed", actualSpeed);

        
        if (moveInput.x > 0f)
        {
            spriteRenderer.flipX = false;
            lastDirection = 1f; 
        }
        else if (moveInput.x < 0f)
        {
            spriteRenderer.flipX = true;
            lastDirection = -1f; 
        }
    }

    
    public float GetLooking()
    {
        return moveInput.x != 0 ? moveInput.x : lastDirection;
    }

    public static PlayerMovement GetInstance()
    {
        return instance;
    }
}
