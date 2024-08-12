using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveButton : MonoBehaviour
{
    public Canvas targetCanvas;
    public KeyCode pressKey = KeyCode.Z;

    private bool canPress = true;
    private float canvasDisableTime = 3f;
    private float cooldownTime = 20f;

    void Start()
    {
        if (targetCanvas != null)
        {
            targetCanvas.enabled = false;
        }
    }

    void Update()
    {
        if (canPress && Input.GetKeyDown(pressKey))
        {
            ToggleCanvasWithDelay();
        }
    }

    void ToggleCanvasWithDelay()
    {
        if (targetCanvas != null)
        {
            targetCanvas.enabled = !targetCanvas.enabled;

            if (targetCanvas.enabled)
            {
                StartCoroutine(DisableCanvasAfterDelay(canvasDisableTime));
                canPress = false;
                Invoke("EnableToggling", cooldownTime);
            }
        }
    }

    IEnumerator DisableCanvasAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (targetCanvas != null && targetCanvas.enabled)
        {
            targetCanvas.enabled = false;
        }
    }

    void EnableToggling()
    {
        canPress = true;
    }
}