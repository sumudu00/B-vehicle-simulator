using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enableGlass : MonoBehaviour
{

    public GameObject glassWindscreen;
    public GameObject crashWindscreen; 

    private bool isCrashWindscreenActive = false; 

    private void Start()
    {
       
      
        glassWindscreen.SetActive(true);
        crashWindscreen.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
    
        if (collision.gameObject.CompareTag("car"))
        {
            if (!isCrashWindscreenActive)
            {
                
                ToggleWindscreens(true);
            }
        }
    }

    private void Update()
    {
        // Toggle the windscreen back to the normal glass when 'F' is pressed
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isCrashWindscreenActive)
            {
                ToggleWindscreens(false);
            }
        }
    }

    private void ToggleWindscreens(bool useCrashWindscreen)
    {
        if (useCrashWindscreen)
        {
            // Activate crash windscreen and deactivate glass windscreen
            glassWindscreen.SetActive(false);
            crashWindscreen.SetActive(true);
            isCrashWindscreenActive = true;
        }
        else
        {
            // Activate glass windscreen and deactivate crash windscreen
            glassWindscreen.SetActive(true);
            crashWindscreen.SetActive(false);
            isCrashWindscreenActive = false;
        }
    }
}


