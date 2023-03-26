using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public Camera playerCamera;
    public CharacterController ch;
    Vector3 moveDirection = Vector3.zero;

    private float walkingSpeed = 5.0f;
    private float runningSpeed = 15.0f;
    private float jumpSpeed = 7.0f;
    private const float gravity = 9.81f;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f; //Angolo limite oltre il quale la camera non può andare

    float rotationX = 0;

    public bool canMove = true;
    void Start()
    {
        ch = GetComponent<CharacterController>();
    }

    void Update()
    {
        //Salvo in 2 variabili la direzione verso cui il player sta guardando
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        bool isRunning = Input.GetKey(KeyCode.LeftShift); //Controllo se il player sta premendo shift per correre

        //Assegno la velocità di movimento corretta a seconda che il player stia correndo o camminando
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * verticalMovement : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * horizontalMovement : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);


        if (Input.GetButton("Jump") && canMove && ch.isGrounded)
            moveDirection.y = jumpSpeed;

        else
            moveDirection.y = movementDirectionY;


        //Se il player non è a terra devo applicargli la gravità
        if (!ch.isGrounded)
            moveDirection.y -= gravity * Time.deltaTime;

        //Muovo il player
        ch.Move(moveDirection * Time.deltaTime);

        //Rotazione del player e della camera
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }
}