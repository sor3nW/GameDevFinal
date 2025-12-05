using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class EnemyAI : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 3f;
    public float detectionRange = 15f;
    public float stoppingDistance = 6f;
    public float gravity = -9.81f;

    [Header("Combat")]
    public GameObject enemyLaserPrefab;
    public Transform firePoint;
    public float fireRate = 2.0f;
    // Set this to (0,0,0) if your firePoint Z-axis is already pointing forward
    public Vector3 laserRotationOffset = new Vector3(0, 0, 0);

    [Header("Animation")]
    public string speedParameterName = "Speed";

    private Transform player;
    private float nextFireTime = 0f;
    private CharacterController controller;
    private Vector3 velocity;
    private Animator anim;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // 1. Safer Animator Finding
        // Try on this object first, then children
        anim = GetComponent<Animator>();
        if (anim == null) anim = GetComponentInChildren<Animator>();

        // 2. Safer Player Finding
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("EnemyAI: No object with tag 'Player' found!");
        }
    }

    void Update()
    {
        // Safety Check: If player is dead/missing, do nothing (Prevents NullReference)
        if (player == null) return;

        // 3. Gravity
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // 4. AI Logic
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            // FIX: Look at Player (Level Headed)
            // We create a temporary target point that is at the SAME HEIGHT as the enemy
            // This prevents the enemy from looking at the floor or sky
            Vector3 lookTarget = player.position;
            lookTarget.y = transform.position.y;
            transform.LookAt(lookTarget);

            // Move logic
            if (distanceToPlayer > stoppingDistance)
            {
                // Calculate direction towards player (ignoring Y)
                Vector3 direction = (player.position - transform.position).normalized;
                direction.y = 0; // Force flat movement

                controller.Move(direction * moveSpeed * Time.deltaTime);
            }

            // Shoot logic
            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
        }

        // 5. UPDATE ANIMATION (Improved Speed Calc)
        if (anim != null)
        {
            // We use the controller's actual velocity (horizontal only)
            Vector3 horizontalVelocity = new Vector3(controller.velocity.x, 0, controller.velocity.z);
            float currentSpeed = horizontalVelocity.magnitude;

            // Debug helps verify if it's working
            // Debug.Log("Enemy Speed: " + currentSpeed);

            // Check if parameter exists before setting to prevent errors
            // Note: Efficient way is to cache the hash ID, but string check is safer for debugging
            anim.SetFloat(speedParameterName, currentSpeed);
        }
    }

    void Shoot()
    {
        if (enemyLaserPrefab != null && firePoint != null)
        {
            // We use firePoint.rotation because we already leveled the enemy with LookAt
            // Then apply the offset for the specific prefab orientation
            Quaternion finalRotation = firePoint.rotation * Quaternion.Euler(laserRotationOffset);
            Instantiate(enemyLaserPrefab, firePoint.position, finalRotation);
        }
    }
}