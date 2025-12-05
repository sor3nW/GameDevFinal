using UnityEngine;

public class StopTimer: MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        // Stop the game timer
        if (Timer.Instance) Timer.Instance.StopTimer();
        // Remove this trigger so it can't be used again
        
    }
}
