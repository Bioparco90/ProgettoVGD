using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float dashForce; //Froza con cui il player fa il dash
    public float moveSpeed; //Massima velocità di movimento
    public float groundDrag; //Attrito col terreno
    public float jumpForce; //Froza con cui il player salta
    public float jumpCooldown; //Cooldown in secondi del salto
    public float dashCooldown; //Cooldown in secondi del dash
    public float airMultiplier; //Attrito con l'aria
    bool readyToJump;
    bool readyToDash;

    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;

    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        dashForce = 1000;
        moveSpeed = 15;
        groundDrag = 4;
        jumpForce = 7;
        jumpCooldown = 1;
        dashCooldown = 3;
        airMultiplier = 1;
        playerHeight = 2f;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;
        readyToDash = true;
    }

    private void Update()
    {
        //Con un raycast controllo se il player sta toccando il layer del terreno
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        MyInput();
        SpeedControl();

        //Gestione dell'attrito
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");


        if (Input.GetKey(KeyCode.Space) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown); //Chiama la funzione resetJump dopo aver aspettato il cooldown del salto
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && readyToDash)
        {
            print("sto dashando");
            readyToDash = false;

            Dash();

            Invoke(nameof(ResetDash), dashCooldown);
        }
    }

    private void MovePlayer()
    {
        //Calcola la direzione del movimento
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //Controlla se il player è a terra e lo fa muovere di conseguenza
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        //Controlla se il player è in aria e lo fa muovere di conseguenza        
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        //Prendo la velocità attuale del player
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //Se è maggiore della velocotà di movimento la resetto al valore massimo
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }

    private void ResetDash()
    {
        readyToDash = true;
    }
    private void Dash()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(orientation.forward * dashForce, ForceMode.Impulse);
    }
}

