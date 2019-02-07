using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanMove : MonoBehaviour {

    private Animator animator;
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Input.GetKey("up"));
        if (Input.GetKey("up"))
        {
            transform.position += transform.forward * 0.01f;
            animator.SetBool("is_runninsg", true);
        }
        else
        {
            animator.SetBool("is_running", false);
        }
        if (Input.GetKey("right"))
        {
            transform.Rotate(0, 10, 0);
        }
        if (Input.GetKey("left"))
        {
            transform.Rotate(0, -10, 0);
        }
    }
}
