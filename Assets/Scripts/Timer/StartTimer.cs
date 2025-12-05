using UnityEngine;

public class StartTimer : MonoBehaviour
{
    public int timeLimit;
    public void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        // Stop the game timer
        if (Timer.Instance) Timer.Instance.StartTimer(timeLimit);
        // Remove this trigger so it can't be used again
    }
}
