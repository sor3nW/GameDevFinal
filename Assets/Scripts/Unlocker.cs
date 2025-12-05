using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Unlocker: MonoBehaviour
{
    [Header("Gate to open if requirement met")]
    public GameObject gate;
    public GameRules gameRules;
    public GameObject fire;

    [Header("Requirement")]
    [Min(0)]
    public int requiredPickups = 4;

    [Header("Layers")]
    public string blockingLayerName = "Blocking";     // included in PlayerMovement.blockingMask
    public string nonBlockingLayerName = "NonBlocking"; // excluded from PlayerMovement.blockingMask

    void Reset()
    {
        var c = GetComponent<Collider>();
        if (c) c.isTrigger = true; // this object is a trigger volume
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (gate == null) return;
        
        int have = GameRules.Instance ? GameRules.Instance.Collected : 0;

        if (have >= requiredPickups)
        {
            OpenGate();
        }
        else
        {
            // Not enough pickups → keep it blocking (optionally play a "locked" sound here)
            BlockGate();
        }
    }
    
    void OpenGate()
    {
        int nonBlock = LayerMask.NameToLayer(nonBlockingLayerName);
        gate.layer = nonBlock;

        var col = gate.GetComponent<Collider>();
        if (col) col.isTrigger = true;

        Destroy(fire);
    }

    void BlockGate()
    {
        int block = LayerMask.NameToLayer(blockingLayerName);
        gate.layer = block;

        var col = gate.GetComponent<Collider>();
        if (col) col.isTrigger = false;
    }
}
