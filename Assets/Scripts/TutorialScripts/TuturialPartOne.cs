using UnityEngine;

public class TuturialPartOne : MonoBehaviour
{
    public GameObject tutorialPartOneUI;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tutorialPartOneUI.SetActive(true);
            Time.timeScale = 0f;
            PauseManager.isPaused = true;
        }
    }
    
}

