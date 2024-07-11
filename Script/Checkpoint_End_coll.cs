using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint_End_coll : MonoBehaviour
{
    public bool triggered = false;
    public BoxCollider boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("RallyCar")||other.CompareTag("car"))
        {
            Debug.Log("Triggered");
            triggered = true;
            boxCollider = GetComponent<BoxCollider>();
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
