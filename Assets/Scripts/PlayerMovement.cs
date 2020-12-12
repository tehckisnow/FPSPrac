﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject textbox;
    public CharacterController controller;
    public bool activePlayer = false;

    public float speed = 12f;
    public float runSpeed = 3f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      if(activePlayer)
        {
            CheckInput();
        }  
    }

    public void CheckInput()
    {
        //groundcheck (to toggle jumping)
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //get input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //runCheck
        float runMod = 1f;
        if(Input.GetButton("Fire3"))
        {
            runMod = runSpeed;
        }

        //apply movement
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * runMod * Time.deltaTime);
        //jump
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        //gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }
}
