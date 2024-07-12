using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clearTheRoad : MonoBehaviour
{
    public Camera maincamera; 

    void Start()
    {
        
        if (maincamera == null)
        {
            maincamera = Camera.main;
        }
    }

    public void OnButtonClicked()
    {
        
        GameObject[] cars = GameObject.FindGameObjectsWithTag("car");

        foreach (GameObject car in cars)
        {
            if (IsInView(car.transform.position))
            {
                car.SetActive(false);
               
            }
        }
    }

    bool IsInView(Vector3 position)
    {
        // Check if the position is within the main camera's view
        if (maincamera == null)
        {
            
            return false;
        }

        Vector3 screenPoint = maincamera.WorldToViewportPoint(position);
        return screenPoint.x >= 0 && screenPoint.x <= 1 && screenPoint.y >= 0 && screenPoint.y <= 1 && screenPoint.z > 0;
    }
}
