using UnityEngine;

public class ContinueScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject tutorialPartOneUI;

    public void continueOn()
    {
        tutorialPartOneUI.SetActive(false);
        Time.timeScale = 1f;
        PauseManager.isPaused = false;
    }
}
