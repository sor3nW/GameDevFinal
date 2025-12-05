using UnityEngine;
using UnityEngine.SceneManagement;
public class GameStart: MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
