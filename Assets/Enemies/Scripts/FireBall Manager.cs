using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallManager : MonoBehaviour
{
    public float fireBallSpeed;

    Transform target;

    private PlayerController player;

    Vector3 direction;

    Rigidbody rb;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("PlayerCollider").GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("PlayerCollider").GetComponent<PlayerController>();
        //direction = transform.parent.transform.forward;
        rb = GetComponent<Rigidbody>();
        fireBallSpeed = 70;
    }



    void Update()
    {
        transform.LookAt(player.transform);
        rb.AddForce(rb.transform.forward * fireBallSpeed, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "PlayerCollider")
        {
            player.takeDamage(20);
        }

        Destroy(gameObject);
    }
}


