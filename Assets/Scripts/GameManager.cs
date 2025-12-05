using UnityEngine;
using TMPro; // Needed for TextMeshPro

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton for easy access

    [Header("UI Reference")]
    public TextMeshProUGUI counterText;

    private int packetsRemaining = 0;

    void Awake()
    {
        // Singleton pattern
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void SetPacketGoal(int count)
    {
        packetsRemaining = count;
        UpdateUI();
    }

    public void CollectPacket()
    {
        packetsRemaining--;
        if (packetsRemaining < 0) packetsRemaining = 0;

        UpdateUI();

        if (packetsRemaining == 0)
        {
            Debug.Log("Room Cleared! Open Exit Gate logic goes here.");
            // You can call a function here to open the exit door
        }
    }

    void UpdateUI()
    {
        if (counterText != null)
        {
            counterText.text = packetsRemaining.ToString();
        }
    }
}