using UnityEngine;
using UnityEngine.UI; // Required for UI

public class EnemyHealth : MonoBehaviour
{
    [Header("Settings")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("References")]
    public Slider healthBar; // Drag your slider here
    public AudioClip hurtSound; // Drag your .mp3 or .wav file here
    public AudioClip deathSound;
    private AudioSource audioSource;

    public GameObject explosionPrefab;
    void Start()
    {
        // Set health to full at start
        currentHealth = maxHealth;

        // Setup the slider
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    public void TakeDamage(float damageAmount)
    {
        // Subtract health
        currentHealth -= damageAmount;

        // Update UI
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        // Check for death
        if (currentHealth <= 0)
        {
            Die();
            PlayDeath(); 
        }
        PlayHurt();
    }

    void Die()
    {
        if (explosionPrefab != null)
        {
            // Spawn the explosion at the enemy's exact position and rotation
            // We store the reference so we can force it to play if needed
            GameObject explosionInstance = Instantiate(explosionPrefab, transform.position, transform.rotation);
            Destroy(explosionInstance, 1.6f);
            // If it's a Particle System that doesn't auto-play, uncomment this:
            // explosionInstance.GetComponent<ParticleSystem>().Play();

            // Note: Since the prefab is set to auto-destroy, we don't need a line to destroy it here.
        }
        // Add explosion sound or particles here later!
        Destroy(gameObject);
    }
    void PlayHurt()
    {
        // Safety Check: Do we have a sound and a speaker?
        if (hurtSound != null && audioSource != null)
        {
            // PlayOneShot allows multiple shots to overlap without cutting each other off
            SoundFXManager.instance.PlaySoundFXClip(hurtSound, transform, 1f);

        }
    }
    void PlayDeath()
    {
        // Safety Check: Do we have a sound and a speaker?
        if (deathSound != null && audioSource != null)
        {
            // PlayOneShot allows multiple shots to overlap without cutting each other off
            SoundFXManager.instance.PlaySoundFXClip(deathSound, transform, 1f);

        }
    }
}