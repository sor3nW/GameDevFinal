using UnityEngine;
using System.Collections;

public class ElectricTrapToggle : MonoBehaviour
{
    [Header("Settings")]
    public float onDuration = 1.0f;  // How long the trap stays ON
    public float offDuration = 1.0f; // How long the trap stays OFF

    // Internal state tracker
    private bool isTrapActive = true;

    void Start()
    {
        // Start the infinite loop
        StartCoroutine(TrapLoop());
    }

    IEnumerator TrapLoop()
    {
        while (true) // Run forever
        {
            // 1. Set Trap State
            SetChildrenActive(isTrapActive);

            // 2. Wait for the appropriate time
            if (isTrapActive)
            {
                yield return new WaitForSeconds(onDuration);
            }
            else
            {
                yield return new WaitForSeconds(offDuration);
            }

            // 3. Flip the state for the next loop (True becomes False, False becomes True)
            isTrapActive = !isTrapActive;
        }
    }

    // Helper function to turn all child objects on or off
    void SetChildrenActive(bool activeState)
    {
        // "transform" refers to the object this script is attached to (arc holder)
        // looping through it gives us every child inside it
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(activeState);
        }
    }
}