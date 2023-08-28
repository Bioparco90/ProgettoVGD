using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public AudioSource takeDamageSound;
    public int healtPoints;
    public float moveSpeed; //Massima velocità di movimento
    public float groundDrag; //Attrito col terreno
    public float jumpForce; //Froza con cui il player salta
    public float jumpCooldown; //Cooldown in secondi del salto
    public float airMultiplier; //Attrito con l'aria
    bool readyToJump;

    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;

    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    public bool tmp = true;
    Vector3 moveDirection;

    Rigidbody rb;
    public TextMeshProUGUI healtText; // Perchè?

    public Hud hud;
    public bool isImmortal;
    public string scene;

    private void Start()
    {
        bool isLoaded = PlayerPrefs.GetString("LoadedGame") == "True";

        healtPoints = 100;
        moveSpeed = 25;
        groundDrag = 4;
        jumpForce = 8;
        jumpCooldown = 1;
        airMultiplier = 1;
        playerHeight = 2f;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;

        isImmortal = PlayerPrefs.HasKey("isImmortal") ? PlayerPrefs.GetString("isImmortal") == "True" : false;
        scene = SceneManager.GetActiveScene().name;

        if (isLoaded)
        {
            LoadPlayer();
            // PlayerPrefs.SetString("LoadedGame", "False"); // Se lo setto qua poi non mi entra nell'if nel gun manager e non mi carica le munizioni
        }
    }

    private void Update()
    {
        if (tmp)
            healtText.SetText("Health: " + healtPoints);

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
        //hud.UpdateHealthText();
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


    public void takeDamage(int damage)
    {
        if (isImmortal)
        {
            damage = 0;
        }

        if (healtPoints - damage <= 0)
        {
            healtPoints = 0;
        }
        else
        {
            healtPoints -= damage;
        }

        takeDamageSound.Play();

    }

    public void healPlayer(int healthAmount)
    {
        healtPoints = healtPoints + healthAmount >= 100 ? 100 : healtPoints + healthAmount;
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        healtPoints = data.health;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
    }
}


