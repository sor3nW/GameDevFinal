using UnityEngine;

public class StartGate : MonoBehaviour
{
    [Header("Room Settings")]
    public int packetsInThisRoom = 5; // Change this number for each room

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            // Update the GameManager
            GameManager.Instance.SetPacketGoal(packetsInThisRoom);

            // Lock it so walking back doesn't reset it
            hasTriggered = true;

            Debug.Log("Entered Room. Collect " + packetsInThisRoom + " packets!");
            Destroy(gameObject); // Optional: Destroy the gate after triggering
        }
    }
}