using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public Transform cam;

    private float mass = 1;
    private float drag = 2;
    public float speed;
    public float jumpSpeed;
    private bool isGrounded = false;
    private int jumpCount;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = mass;
        rb.drag = drag;
        speed = 0.5f;
        jumpSpeed = 0;
        jumpCount = 0;

    }

    void FixedUpdate()
    {
        Debug.Log("diocane");
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;

        camForward.y = 0;
        camRight.y = 0;

        Vector3 forwardRelative = verticalInput * camForward;
        Vector3 rightRelative = horizontalInput * camRight;

        Vector3 movementDirection = forwardRelative + rightRelative;

        Vector3 movement = new Vector3(movementDirection.x * speed, 0f, movementDirection.z * speed);


        if (Input.GetKeyDown("space") && isGrounded)
        {
            switch (jumpCount)
            {
                case 0:
                    jumpSpeed = 3.0f;
                    rb.AddForce(new Vector3(0f, jumpSpeed, 0f), ForceMode.Impulse);
                    jumpCount++;
                    Debug.Log("Primo");
                    break;

                case 1:
                    jumpSpeed = 1.0f;
                    rb.AddForce(new Vector3(0f, jumpSpeed, 0f), ForceMode.Impulse);
                    jumpCount++;
                    Debug.Log("Secondo");
                    break;

                case 2:
                    isGrounded = false;
                    break;
            }
        }

        if (isGrounded)
        {
            rb.AddForce(movement, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Floor"))
        {
            isGrounded = true;
            if (jumpCount != 0)
                jumpCount = 0;
        }
    }

   /* private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Floor"))
            isGrounded = false;
        
    }*/
}

