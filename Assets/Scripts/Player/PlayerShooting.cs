using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("References")]
    public GameObject laserPrefab;
    public Transform firePoint;
    public Camera mainCam;

    // NEW: Audio Reference
    public AudioClip laserSound; // Drag your .mp3 or .wav file here
    private AudioSource audioSource; // We will find this automatically

    void Start()
    {
        if (mainCam == null) mainCam = Camera.main;

        // Find the AudioSource we just added
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {   
        if (PauseManager.isPaused) return;
        if (Input.GetButtonDown("Fire1"))
        {
            ShootDirectlyAtCursor();
        }
    }

    void ShootDirectlyAtCursor()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 targetPoint = hit.point;
            targetPoint.y = firePoint.position.y; // Level out height

            Vector3 direction = targetPoint - firePoint.position;
            Quaternion laserRotation = Quaternion.LookRotation(direction);

            Instantiate(laserPrefab, firePoint.position, laserRotation);

            // NEW: Play the sound!
            SoundFXManager.instance.PlaySoundFXClip(laserSound, transform, 0.75f);
        }
    }

    
}