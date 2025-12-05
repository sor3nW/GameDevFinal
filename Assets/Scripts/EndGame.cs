using UnityEngine;
using UnityEngine.SceneManagement;
public class EndGame : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        SceneManager.LoadScene("GameOverScene");

    }
}
