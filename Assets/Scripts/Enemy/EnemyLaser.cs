using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    public float speed = 8f;   // Slower than player laser (which was 20)
    public int damage = 10;
    public float lifeTime = 5f;

    void Start()
    {
        // Destroy after 5 seconds to keep game clean
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Move forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // Ignore the enemy that shot this
        if (other.CompareTag("Enemy") || other.CompareTag("Trap")) return;

        // If we hit the Player
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            // Destroy the laser
            Destroy(gameObject);
        }
        // If we hit a Wall (Blocking layer)
        else if (other.gameObject.layer == LayerMask.NameToLayer("Blocking"))
        {
            Destroy(gameObject);
        }
    }
}