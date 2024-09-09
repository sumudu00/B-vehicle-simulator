using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMistIntensity : MonoBehaviour
{
    public static float MistSliderVal = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void MistSliderChanged(float val)
    {
        MistSliderVal = val * 100;
        //RainScript.RainIntensity = val;
    }

    // Update is called once per frame
    void Update()
    {
        //RainIntensity = RainSliderVal;
    }
}
