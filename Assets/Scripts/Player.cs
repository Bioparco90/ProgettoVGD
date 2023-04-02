using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController ch;    
    private float walkingSpeed = 7.0f;
    private float runningSpeed = 15.0f;
    private float jumpSpeed = 10.0f;
    private const float gravity = 9.81f;
    Vector3 move;

    void Start()
    {
        ch = GetComponent<CharacterController>();
    }

    void Update()
    {
        //Salvo la direzione verso il quale il player sta guardando
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        bool isRunning = Input.GetKey(KeyCode.LeftShift); //Controllo se il player sta premendo shift per correre

        //Assegno la velocità di movimento corretta a seconda che il player stia correndo o camminando
        float curSpeedX = (isRunning ? runningSpeed : walkingSpeed) * verticalMovement;
        float curSpeedY = (isRunning ? runningSpeed : walkingSpeed) * horizontalMovement;
        float movementDirectionY = move.y;
        move = (forward * curSpeedX) + (right * curSpeedY);


        if (Input.GetButton("Jump") && ch.isGrounded)
            move.y = jumpSpeed;

        else
            move.y = movementDirectionY;


        //Se il player non è a terra gli applico la gravità
        if (!ch.isGrounded)
            move.y -= gravity * Time.deltaTime;

        //Muovo il player
        ch.Move(move * Time.deltaTime);
    }
}

