using UnityEngine;

public class SpecialEnemy : MonoBehaviour
{
    public Transform playerTransform; // Player object's Transform
    public float normalSpeed = 2f; // Normal movement speed
    public float chargeSpeed = 8f; // Charge speed
    public float detectionRadius = 10f; // Range for player detection
    public float chargeDuration = 1f; // Duration of charge
    public float chargeCooldown = 2f; // Cooldown time for charge
    public LayerMask playerLayer; // Layer where the player is located

    private float currentSpeed; // Current movement speed
    private bool isCharging = false; // Whether currently charging
    private bool isCooldown = false; // Whether in cooldown state
    private Vector3 chargeTarget; // Target position for charge
    private float chargeEndTime = 0f; // Time when charge ends
    private float cooldownEndTime = 0f; // Time when cooldown ends
    private SpriteRenderer spriteRenderer; // SpriteRenderer used to change enemy color

    void Start()
    {
        currentSpeed = normalSpeed;
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the enemy's SpriteRenderer
    }

    void Update()
    {
        // Check if the player is within range
        Collider2D[] detectedPlayers = Physics2D.OverlapCircleAll(transform.position, detectionRadius, playerLayer);
        bool playerInRange = detectedPlayers.Length > 0;

        // Switch to charging state
        if (playerInRange && !isCharging && !isCooldown)
        {
            StartCharge();
        }

        // Check if charge has ended
        if (isCharging && (Time.time >= chargeEndTime || Vector3.Distance(transform.position, chargeTarget) < 0.1f))
        {
            EndCharge();
        }

        // Check if cooldown has ended
        if (isCooldown && Time.time >= cooldownEndTime)
        {
            isCooldown = false;
        }

        // Move the enemy
        MoveTowardsChargeTarget(playerInRange);
    }

    void MoveTowardsChargeTarget(bool playerInRange)
    {
        if (isCharging || playerInRange)
        {
            // If charging or player is in range, move towards the target
            Vector2 direction = (chargeTarget - transform.position).normalized;
            transform.position += (Vector3)(direction * currentSpeed * Time.deltaTime);
        }
    }

    void StartCharge()
    {
        spriteRenderer.color = Color.blue;
        isCharging = true;
        currentSpeed = chargeSpeed;
        chargeTarget = playerTransform.position; // Lock onto player's current position
        chargeEndTime = Time.time + chargeDuration;
    }

    void EndCharge()
    {
        isCharging = false;
        currentSpeed = normalSpeed;
        isCooldown = true;
        cooldownEndTime = Time.time + chargeCooldown; // Set cooldown time
        spriteRenderer.color = Color.white;
    }

    // Draw visual debugging information for the detection range
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
