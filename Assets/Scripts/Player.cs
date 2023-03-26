using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController ch;
    private float movementSpeed = 5.0f;
    private float jumpSpeed = 7.0f;
    private const float gravity = 9.81f;
    float vspeed = 0;
    void Start()
    {
        ch = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalMovement, 0f, verticalMovement)*movementSpeed;

        if (ch.isGrounded)
        {
            float jump = Input.GetAxis("Jump") * jumpSpeed;
            vspeed = jump;
        }

        vspeed -= gravity*Time.deltaTime;
        movement.y = vspeed;
        ch.Move(movement * Time.deltaTime);
    }
}

