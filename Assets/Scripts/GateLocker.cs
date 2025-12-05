using UnityEngine;

public class GateLocker : MonoBehaviour
{
    [Header("Gate to lock after crossing")]
    public GameObject gate;

    [Header("Layers")]
    public string blockingLayerName = "Blocking"; // must be included in PlayerMovement.blockingMask

    void Reset()
    {
        var c = GetComponent<Collider>();
        if (c) c.isTrigger = true; // this object is a trigger volume
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (gate != null)
        {
            // Make the green gate BLOCKING going forward
            int blockingLayer = LayerMask.NameToLayer(blockingLayerName);
            gate.layer = blockingLayer;

            var col = gate.GetComponent<Collider>();
            if (col) col.isTrigger = false;
        }

        // Reset pickup count for the new level/run
        if (GameRules.Instance != null) GameRules.Instance.ResetPickups();

        // Remove this trigger so it can't be used again
        Destroy(gameObject);
    }
}
