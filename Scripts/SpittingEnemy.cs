using UnityEngine;

public class SpittingEnemy : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab for the projectile
    Transform playerTransform; // Player object's Transform
    public float detectionRadius = 15f; // Range for player detection
    public float spitCooldown = 3f; // Interval between spitting projectiles
    public LayerMask playerLayer; // Layer where the player is located

    private float nextSpitTime = 0f; // Time of the next projectile spit

    // Set the player's Transform
    public void SetPlayer(Transform player)
    {
        playerTransform = player;
    }

    void Update()
    {
        // Check if the player is within range
        Collider2D[] detectedPlayers = Physics2D.OverlapCircleAll(transform.position, detectionRadius, playerLayer);
        bool playerInRange = detectedPlayers.Length > 0;

        // Check if it's time to spit a projectile
        if (playerInRange && Time.time >= nextSpitTime)
        {
            StartSpitting();
        }
    }

    void StartSpitting()
    {
        // Lock onto the player's current position
        Vector3 targetPosition = playerTransform.position;
        Vector2 direction = (targetPosition - transform.position).normalized;

        // Create a projectile and set its direction
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            projectileScript.SetDirection(direction);
        }

        // Update the time for the next projectile spit
        nextSpitTime = Time.time + spitCooldown;
    }

    // Draw visual debugging information for the detection range
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
