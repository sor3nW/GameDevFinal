using UnityEngine;
using UnityEngine.Audio; // for AudioMixerGroup

[RequireComponent(typeof(Collider))]
public class Pickup : MonoBehaviour
{
    [Header("SFX")]
    public AudioClip collectClip;


    public bool destroyOnCollect = true;

    bool _collected;

    void Reset() { GetComponent<Collider>().isTrigger = true; }

    void OnTriggerEnter(Collider other)
    {
        if (_collected || !other.CompareTag("Player")) return;
        _collected = true;

        if (GameRules.Instance != null) GameRules.Instance.RegisterPickup();
        SoundFXManager.instance.PlaySoundFXClip(collectClip, transform, 2f);
        Collect();

        if (destroyOnCollect) Destroy(gameObject);
        else gameObject.SetActive(false);
    }
    void Collect()
    {
        Debug.Log("Data Packet Collected!");

        // NEW: Tell GameManager we found one
        if (GameManager.Instance != null)
        {
            GameManager.Instance.CollectPacket();
        }

        // Play sound if you have one
        // AudioSource.PlayClipAtPoint(pickupSound, transform.position);

        Destroy(gameObject);
    }

}
