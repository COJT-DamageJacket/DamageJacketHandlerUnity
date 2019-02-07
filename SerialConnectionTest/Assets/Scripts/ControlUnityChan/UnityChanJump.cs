using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanJump : MonoBehaviour {

    CharacterController characterController;

	// Use this for initialization
	void Start () {
        characterController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        Animator animator = GetComponent<Animator>();   // ...(1)

        if (Input.GetKey(KeyCode.X))   // ...(2)
        {
            animator.SetBool("Jump", true);
        }

        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);   // ...(3)
        if (state.IsName("Locomotion.Jump") || state.IsName("Locomotion.NormalJump"))   // ...(4)
        {
            animator.SetBool("Jump", false);
        }

        characterController.height = animator.GetFloat("JumpHeight")*1.9f;
    }
}
