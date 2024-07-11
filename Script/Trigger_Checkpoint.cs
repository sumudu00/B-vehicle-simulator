using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Checkpoint : MonoBehaviour
{
    public float stopTime = 2;
    public bool triggered = false;
    public BoxCollider boxCollider;
    string GoString = "TrafficOff";
    string StopString = "TrafficOn";
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("RallyCar"))
        {
            Debug.Log("Triggered by Rally Car");
            triggered = true;
            boxCollider = GetComponent<BoxCollider>();
        }
        if (other.CompareTag("car"))
        {
            Debug.Log("Triggered By Ai Car");
            triggered = true;
            boxCollider = GetComponent<BoxCollider>();
            StartCoroutine(DelayedActiveTrue());
            gameObject.tag = StopString;
        }


        /*/else
        {
            triggered = false;
            Debug.Log("trigger Off");
        }*/

    }
    private System.Collections.IEnumerator DelayedActiveTrue()
    {
        yield return new WaitForSeconds(stopTime);
        gameObject.tag = GoString;
        Debug.Log("Tag Changed Traffic off");
    }

    private void OnTriggerExit(Collider other)
    {
        gameObject.tag = StopString;
        Debug.Log("TRaffic on");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
