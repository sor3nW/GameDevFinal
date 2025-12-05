using UnityEngine;

public class LaserTrap : MonoBehaviour
{
    [Header("Setup")]
    public Transform pointA;        // The starting empty object
    public Transform pointB;        // The ending empty object
    public GameObject laserWallPrefab; // The box/laser wall to spawn

    [Header("Settings")]
    public float speed = 3f;
    public float waitTime = 0.5f;   // Optional pause at start

    private GameObject currentWall;
    private Vector3 targetPos;
    private float pauseTimer = 0f;

    void Start()
    {
        if (pointA == null || pointB == null || laserWallPrefab == null)
        {
            Debug.LogError("LaserTrap: Missing assignments in Inspector!");
            return;
        }

        // Spawn the wall at Point A initially
        currentWall = Instantiate(laserWallPrefab, pointA.position, Quaternion.identity);

        // Optional: Parent it to this script holder to keep hierarchy clean
        currentWall.transform.SetParent(transform);

        // Set initial target to B
        targetPos = pointB.position;
    }

    void Update()
    {
        if (currentWall == null) return;

        // Check if we are waiting
        if (pauseTimer > 0)
        {
            pauseTimer -= Time.deltaTime;
            return;
        }

        // Move the wall towards the target (Always towards B)
        currentWall.transform.position = Vector3.MoveTowards(currentWall.transform.position, targetPos, speed * Time.deltaTime);

        // Check if we reached the target (Point B)
        if (Vector3.Distance(currentWall.transform.position, targetPos) < 0.01f)
        {
            // Instead of switching target, we TELEPORT back to A
            currentWall.transform.position = pointA.position;

            // Ensure target remains B (redundant but safe)
            targetPos = pointB.position;

            // Start waiting before launching again
            pauseTimer = waitTime;
        }
    }

    // Visualize the path in the Scene view
    void OnDrawGizmos()
    {
        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(pointA.position, pointB.position);
            Gizmos.DrawSphere(pointA.position, 0.2f);
            Gizmos.DrawSphere(pointB.position, 0.2f);
        }
    }
}