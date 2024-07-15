using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enbleButton : MonoBehaviour
{
    public Canvas targetCanvas;


    void Start()
    {

        if (targetCanvas != null)
        {
            targetCanvas.enabled = false;
        }
    }

    void Update()
    {
        // Check if the toggle key is pressed
        if (Input.GetKeyDown(KeyCode.Z))
        {
            // Toggle the active state of the targetCanvas
            if (targetCanvas != null)
            {
                targetCanvas.enabled = !targetCanvas.enabled;
            }
        }
    }
}
