using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float speed = 5f; // Movement speed
    public float attackDistance = 5f; // Distance at which the boss starts attacking
    public float attackDuration = 2f; // Duration of the attack
    public float attackCooldown = 3f; // Cooldown time between attacks
    public float patternDuration = 5f; // Duration of each movement pattern
    public float patternCooldown = 2f; // Cooldown time between movement patterns
    public float patternDistance = 2f; // Maximum distance to move during a movement pattern

    private Transform playerTransform; // Reference to the player's transform
    private Vector3 targetPosition; // Current target position for movement
    private bool isAttacking = false; // Flag to indicate if the boss is attacking
    private bool isMoving = false; // Flag to indicate if the boss is moving
    private float attackStartTime = 0f; // Time when the attack started
    private float patternStartTime = 0f; // Time when the current pattern started

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Find the player object and get its transform
        targetPosition = transform.position; // Set the initial target position to the boss's current position
    }

    private void Update()
    {
        if (isAttacking)
        {
            // If the boss is attacking, check if the attack duration has elapsed and then end the attack
            if (Time.time >= attackStartTime + attackDuration)
            {
                EndAttack();
            }
        }
        else
        {
            // If the boss is not attacking, check if it should start attacking
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
            if (distanceToPlayer <= attackDistance)
            {
                StartAttack();
            }
            else
            {
                // If the boss is not attacking and not moving, start a new movement pattern
                if (!isMoving)
                {
                    StartPattern();
                }

                // Move towards the target position
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

                // Check if the boss has reached the target position
                if (transform.position == targetPosition)
                {
                    isMoving = false; // Stop moving
                }
            }
        }
    }

    private void StartAttack()
    {
        isAttacking = true; // Set the attacking flag
        attackStartTime = Time.time; // Record the start time of the attack
        // TODO: Implement the attack logic here
    }

    private void EndAttack()
    {
        isAttacking = false; // Clear the attacking flag
        StartCoroutine(AttackCooldownCoroutine()); // Start the attack cooldown coroutine
    }

    private IEnumerator AttackCooldownCoroutine()
    {
        yield return new WaitForSeconds(attackCooldown); // Wait for the attack cooldown period
        isMoving = false; // Stop moving
    }

    private void StartPattern()
    {
        isMoving = true; // Start moving
        patternStartTime = Time.time; // Record the start time of the pattern
        targetPosition = GetRandomPatternPosition(); // Generate a new random target position
    }

    private Vector3 GetRandomPatternPosition()
    {
        Vector3 direction = Random.insideUnitCircle.normalized; // Get a random direction
                                                                // Limit the distance between the boss and the player
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        float maxDistance = Mathf.Min(patternDistance, distanceToPlayer - attackDistance);
        return transform.position + direction * Random.Range(0f, maxDistance); // Move in that direction for a random distance within the limit
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}
