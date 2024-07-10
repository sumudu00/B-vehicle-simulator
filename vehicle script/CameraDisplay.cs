using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDisplay : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < Display.displays.Length; i++)
        {
            Debug.Log("displays connected: " + Display.displays.Length);
            Display.displays[i].Activate();
        }
    }
}
	

