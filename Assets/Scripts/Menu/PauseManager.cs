using UnityEngine;
using UnityEngine.SceneManagement; // For reloading or quitting

public class PauseManager : MonoBehaviour
{
    [Header("References")]
    public GameObject pauseMenuUI; // Drag the "PauseMenu" panel here
    public GameObject AudioMenuUI;
    public static bool isPaused = false;
    void Start()
    {
        // 1. Force unpause on start so we don't get stuck
        Time.timeScale = 1f;
        isPaused = false;
        if(pauseMenuUI != null) pauseMenuUI.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        { 
            Debug.Log("E Key Pressed - Toggling Pause");
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false); // Hide Menu
        Time.timeScale = 1f;          // Unfreeze Game
        isPaused = false;

        // Optional: Lock cursor again if your game uses mouse aiming
        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
    }
    public void OpenAudioSettings()
    {
        AudioMenuUI.SetActive(true); // Show Audio Menu
        pauseMenuUI.SetActive(false); // Hide Pause Menu
    }
    public void CloseAudioSettings()
    {
        AudioMenuUI.SetActive(false); // Hide Audio Menu
        pauseMenuUI.SetActive(true); // Show Pause Menu
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);  // Show Menu
        Time.timeScale = 0f;          // Freeze Game
        isPaused = true;

        // Unlock cursor so you can click buttons
        // Cursor.lockState = CursorLockMode.None;
        // Cursor.visible = true;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;

    }
}