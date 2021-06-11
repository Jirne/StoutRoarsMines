using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public CharacterController2D controller;
    public Animator animator;
    public float runSpeed = 40f;


    float horizontalMove = 0f;
    bool jump = false;

    // Update is called once per frame
    void Update() {
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if (Input.GetButtonDown("Jump")) {
            jump = true;
        }

    }

    public void OnLanding() {
    }

    private void FixedUpdate() {
        //Move the character
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

}