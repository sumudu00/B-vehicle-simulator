using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint_Gate : MonoBehaviour
{
    public GameObject targetObject;
    public Animator animator;
    public Trigger_Checkpoint Checkpoint_Entry;
    public Checkpoint_End_coll Checkpoint_Exit;
    public float stopTime = 22f;
    private float AnimStop_Time = 1f;
    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("IsMoving", false);
        //animator.SetBool("IsStop", false);
    }
    // Update is called once per frame
    void Update()
    {
        animator = GetComponent<Animator>();

        if (Checkpoint_Entry.triggered)
        {
            StartCoroutine(DelayedActiveTrue());
        }
        if (Checkpoint_Exit.triggered)
        {
            animator.SetBool("IsMoving", false);
            StartCoroutine(DeActive());
        }
    
        //if (Input.GetKeyDown(KeyCode.N))
        //{
        //    animator.SetBool("IsMoving", true);
        //}

    }

    private System.Collections.IEnumerator DelayedActiveTrue()
    {
        yield return new WaitForSeconds(stopTime);
        animator.SetBool("IsMoving", true);
    }


    private System.Collections.IEnumerator DeActive()
    { 
        Checkpoint_Entry.triggered = false;
        //animator.SetBool("IsStop", true);
        yield return new WaitForSeconds(AnimStop_Time);
        animator.SetBool("IsMoving", false);
        Checkpoint_Exit.triggered = false;
    }
    


    //void RotateObject()
    //{
    //    if (targetObject != null)
    //    {
    //        animator.SetBool("IsMoving", true);

    //    }
    //    else
    //    {
    //        Debug.LogWarning("Target object is not assigned!");
    //    }
    //}

}
