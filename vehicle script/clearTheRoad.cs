using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clearTheRoad : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main; // Assuming your camera is tagged as "MainCamera"
    }

    void Update()
    {
        // Check if 'C' key is pressed
        if (Input.GetKeyDown(KeyCode.G))
        {
            // Get all the cubes in the scene (or objects with cube-shaped colliders)
            GameObject[] cubes = GameObject.FindGameObjectsWithTag("car");

            foreach (GameObject cube in cubes)
            {
                // Check if the cube is within the camera's view
                if (IsInView(cube.transform.position))
                {
                    cube.SetActive(false); // Disable the cube (make it vanish)
                }
            }
        }
    }

    bool IsInView(Vector3 position)
    {
        // Check if the position is within the camera's view
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(position);
        return screenPoint.x >= 0 && screenPoint.x <= 1 && screenPoint.y >= 0 && screenPoint.y <= 1 && screenPoint.z > 0;
    }
}
