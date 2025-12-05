using UnityEngine;

public class GoldenPacket : MonoBehaviour
{
    public GameObject Winner;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Winner.SetActive(true);
            Time.timeScale = 0f; // Pause the game
        }
    }
}
