using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AITarget : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent agent;
    private Animator m_animator;

    [Header("Settings")]
    public float detectionRange = 15f; // Start chasing if player is this close
    public float attackRange = 5f;     // Stop moving and shoot if player is this close
    public float fireRate = 2.0f;      // Time between shots

    [Header("Combat")]
    public GameObject enemyLaserPrefab;
    public Transform firePoint;

    private float nextFireTime = 0f;

    public AudioClip laserSound; // Drag your .mp3 or .wav file here
    private AudioSource audioSource; // We will find this automatically
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        m_animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        // Safety check: Find player if target is not assigned
        if (target == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null) target = playerObj.transform;
        }
    }

    void Update()
    {
        if (target == null) return;

        float distance = Vector3.Distance(transform.position, target.position);

        // 1. IDLE STATE: Player is too far away
        if (distance > detectionRange)
        {
            agent.isStopped = true;
            m_animator.SetBool("isRunning", false);
            m_animator.SetBool("isAttacking", false);
        }
        // 2. ATTACK STATE: Player is within firing range (Highest Priority)
        else if (distance <= attackRange)
        {
            agent.velocity = Vector3.zero; // Kill momentum instantly
            
            FaceTarget(); // Rotate to aim

            m_animator.SetBool("isRunning", false);
            m_animator.SetBool("isAttacking", true);

            // Fire Logic
            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
        }
        // 3. CHASE STATE: Player is visible but far away
        else 
        {
            agent.isStopped = false;
            agent.SetDestination(target.position);
            
            m_animator.SetBool("isRunning", true);
            m_animator.SetBool("isAttacking", false);
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        direction.y = 0; // Keep rotation flat
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void Shoot()
    {
        if (enemyLaserPrefab != null && firePoint != null)
        {
            // Instantiate the laser
            Instantiate(enemyLaserPrefab, firePoint.position, firePoint.rotation);
            SoundFXManager.instance.PlaySoundFXClip(laserSound, transform, 0.75f);

        }
    }
    

}