using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI; // Needed for UI

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("UI Reference")]
    public Slider healthSlider; // Drag your UI Slider here
    public AudioClip hurtSound; // Drag your .mp3 or .wav file here
    private AudioSource audioSource;
    void Start()
    {
        currentHealth = maxHealth;
        UpdateUI();
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth < 0) currentHealth = 0;

        UpdateUI();

        if (currentHealth <= 0)
        {
            Die();
        }
        SoundFXManager.instance.PlaySoundFXClip(hurtSound, transform, 1f);

    }

    void UpdateUI()
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    void Die()
    {
        Debug.Log("Player Died!");

        // Since you already have a Respawn system, let's use it!
        PlayerRespawn respawnScript = GetComponent<PlayerRespawn>();
        if (respawnScript != null)
        {
            // Reset Health
            currentHealth = maxHealth;
            UpdateUI();

            // Teleport back to checkpoint
            respawnScript.Respawn();
        }
        else
        {
            // Fallback if no respawn script exists
            // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            gameObject.SetActive(false);
        }
    }
    void PlayHurt()
    {
        // Safety Check: Do we have a sound and a speaker?
        if (hurtSound != null && audioSource != null)
        {
            // PlayOneShot allows multiple shots to overlap without cutting each other off
            audioSource.PlayOneShot(hurtSound);
        }
    }
    
}