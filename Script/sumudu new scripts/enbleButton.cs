using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enbleButton : MonoBehaviour
{
    public Canvas targetCanvas; // Assign the Canvas in the Inspector
    public KeyCode toggleKey = KeyCode.Z; // Key to toggle the Canvas

    private bool canToggle = true; // Flag to control toggle cooldown

    void Start()
    {
        // Ensure the targetCanvas starts in a disabled state
        if (targetCanvas != null)
        {
            targetCanvas.enabled = false;
        }
    }

    void Update()
    {
        // Check if the toggle key is pressed and toggling is allowed
        if (Input.GetKeyDown(toggleKey) && canToggle)
        {
            ToggleCanvasWithDelay();
        }
    }

    void ToggleCanvasWithDelay()
    {
        // Toggle the active state of the targetCanvas
        if (targetCanvas != null)
        {
            targetCanvas.enabled = !targetCanvas.enabled;

            if (targetCanvas.enabled)
            {
                // Schedule the canvas to disable after 3 seconds
                Invoke("DisableCanvas", 3f);

                // Disable toggling until cooldown period is over
                canToggle = false;
                Invoke("EnableToggling", 5f);
            }
        }
    }

    void DisableCanvas()
    {
        if (targetCanvas != null && targetCanvas.enabled)
        {
            targetCanvas.enabled = false;
        }
    }

    void EnableToggling()
    {
        canToggle = true;
    }
}
