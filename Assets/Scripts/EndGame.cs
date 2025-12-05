using UnityEngine;
using UnityEngine.SceneManagement;
public class EndGame : MonoBehaviour
{
    public void EndGameFunc()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;

    }
    public void RestartGame()
    {
        // 1. Unpause time first (mandatory for scene loading to work properly)
        Time.timeScale = 1f;

        // 2. Get the name of the current scene
        string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        // 3. Load the scene by its name, which restarts it
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneName);
    }
}
