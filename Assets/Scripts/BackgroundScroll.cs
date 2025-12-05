using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    // Controls how fast the code falls. 
    // Positive numbers move down, negative move up.
    public float scrollSpeed = 0.5f;

    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        // Calculate the new offset based on time and speed
        // We alter the 'y' value to move vertically
        float offset = Time.time * scrollSpeed;

        // Apply the offset to the material
        rend.material.mainTextureOffset = new Vector2(0, -offset);
    }
}
