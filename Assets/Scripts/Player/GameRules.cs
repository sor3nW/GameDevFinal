using UnityEngine;

public class GameRules : MonoBehaviour
{
    public static GameRules Instance { get; private set; }

    // How many pickups the player currently has in THIS level/run
    public int Collected { get; private set; } = 0;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        // Optional: DontDestroyOnLoad(gameObject);
    }

    public void RegisterPickup() => Collected++;

    public void ResetPickups() => Collected = 0;

}
