using UnityEngine;

public class LaserProjectile : MonoBehaviour
{
    public float speed = 20f;
    public float maxDistance = 10f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Move the laser forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Check distance to destroy if it flies too far without hitting anything
        float distanceTravelled = Vector3.Distance(startPosition, transform.position);
        if (distanceTravelled >= maxDistance)
        {
            Destroy(gameObject);
        }
    }
   
    void OnTriggerEnter(Collider other)
    {
        // 1. Check for Walls (Blocking Layer)
        if (other.gameObject.layer == LayerMask.NameToLayer("Blocking"))
        {
            Destroy(gameObject);
            return; // Stop reading code
        }

        // 2. Check for Enemy
        // We look for the "EnemyHealth" script on the object we hit
        EnemyHealth enemy = other.GetComponentInParent<EnemyHealth>();
        if (enemy != null)
        {
            // Deal 20 damage (or whatever number you want)
            enemy.TakeDamage(20);

            // Destroy the laser so it doesn't go through them
            Destroy(gameObject);
        }
    }
}