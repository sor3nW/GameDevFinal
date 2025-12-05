using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [Tooltip("Target to follow (e.g., the Player).")]
    public Transform target;

    [Tooltip("Offset from the player (x,y,z). Example: (0, 10, -10) for a top-down view.")]
    public Vector3 offset = new Vector3(0f, 10f, -10f);

    [Tooltip("How smoothly the camera follows.")]
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (target == null) return;

        // Follow the position only
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Optionally: keep a fixed angle
        // If you want the camera never to rotate, just leave it as-is in the scene
        // If you want it to always look at the player:
        transform.LookAt(target);
    }
}
