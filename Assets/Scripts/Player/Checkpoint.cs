using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject gate;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that hit me is the Player
        if (other.CompareTag("Player"))
        {
            // Find the respawn script on the player
            PlayerRespawn playerRespawn = other.GetComponent<PlayerRespawn>();

            if (playerRespawn != null)
            {
                // Tell the player: "This is your new home"
                playerRespawn.SetRespawnPoint(transform.position);

                // Optional: Turn off this checkpoint so it doesn't trigger again
                // gameObject.SetActive(false); 
            }
        }
        gate.layer = LayerMask.NameToLayer("NonBlocking");
        var col = gate.GetComponent<Collider>();
        if (col) col.isTrigger = true;
    }
}
