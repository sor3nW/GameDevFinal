using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class TestPlayerMovement : MonoBehaviour
{
    [Header("Setup")]
    public Transform modelTransform; // Drag the "Jammo_Low" or "Armature" object here!

    [Header("Input")]
    public InputActionReference moveAction; // Vector2 (WASD)

    [Header("Movement")]
    public float moveSpeed = 8f;
    public float acceleration = 100f;
    public float deceleration = 100f;

    [Header("Gravity")]
    public float gravity = -25f;      // units/sec^2
    public float groundedStick = -2f; // small downward push to keep grounded
    public float inputDeadzone = 0.2f;// ignore tiny noise (prevents random spins)

    // RENAMED VARIABLE TO FIX CONFLICT
    private CharacterController myController;
    private Vector3 velocityXZ; // smoothed horizontal velocity
    private float verticalVel;  // manual vertical velocity

    void Awake() { myController = GetComponent<CharacterController>(); }
    void OnEnable() { if (moveAction) moveAction.action.Enable(); }
    void OnDisable() { if (moveAction) moveAction.action.Disable(); }

    void Update()
    {
        // --- 1) INPUT ---
        Vector2 raw = moveAction ? moveAction.action.ReadValue<Vector2>() : Vector2.zero;
        if (raw.magnitude < inputDeadzone) raw = Vector2.zero; // stop noise
        if (raw.sqrMagnitude > 1f) raw.Normalize();           // no faster diagonal

        // --- 2) HORIZONTAL VELOCITY (smooth accel/decel) ---
        Vector3 desired = new Vector3(raw.x, 0f, raw.y) * moveSpeed;
        float a = (raw.sqrMagnitude > 0f) ? acceleration : deceleration;
        velocityXZ = Vector3.MoveTowards(velocityXZ, desired, a * Time.deltaTime);

        // --- 3) VERTICAL (manual gravity) ---
        if (myController.isGrounded && verticalVel < 0f) verticalVel = groundedStick; // keeps CC grounded
        verticalVel += gravity * Time.deltaTime;

        // --- 4) MOVE ---
        Vector3 motion = velocityXZ * Time.deltaTime;
        motion.y = verticalVel * Time.deltaTime;
        myController.Move(motion);

        // --- 5) FACE (Rotate Model Only) ---
        if (raw.sqrMagnitude > 0f)
        {
            float angle = Mathf.Atan2(raw.x, raw.y) * Mathf.Rad2Deg; // 0° = +Z
            float snapped = Mathf.Round(angle / 45f) * 45f;          // 8 directions

            // NEW: Rotate only the visual model, if assigned
            if (modelTransform != null)
            {
                modelTransform.localRotation = Quaternion.Euler(0f, snapped, 0f);
            }
            else
            {
                // Fallback if you forgot to assign it
                transform.rotation = Quaternion.Euler(0f, snapped, 0f);
            }
        }
    }
}