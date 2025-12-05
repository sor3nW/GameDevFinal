using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    // Store the location we should go back to
    private Vector3 currentRespawnPoint;

    // Optional: How much damage to take when respawning
    public int respawnDamage = 10;

    void Start()
    {
        // By default, the starting line is the first checkpoint
        currentRespawnPoint = transform.position;
    }

    // This function is called by the Checkpoint objects
    public void SetRespawnPoint(Vector3 newPosition)
    {
        currentRespawnPoint = newPosition;
        Debug.Log("Checkpoint Updated!");
    }

    // This detects when we hit a trap
    private void OnTriggerEnter(Collider other)
    {
        // You must tag your electric traps or spikes as "Trap"
        if (other.CompareTag("Trap"))
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        Debug.Log("Died! Respawning...");

        // 1. Take Damage Logic (NEW)
        PlayerHealth healthScript = GetComponent<PlayerHealth>();
        if (healthScript != null)
        {
            healthScript.TakeDamage(respawnDamage);
        }

        // 2. Disable Character Controller briefly
        CharacterController cc = GetComponent<CharacterController>();
        if (cc != null) cc.enabled = false;

        // 3. Teleport the player
        transform.position = currentRespawnPoint;

        // 4. Re-enable Character Controller
        if (cc != null) cc.enabled = true;

        // 5. Reset Physics (if using Rigidbody)
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero; // Note: In Unity 6 this is linearVelocity, in older versions it's velocity
            rb.angularVelocity = Vector3.zero;
        }
    }
}