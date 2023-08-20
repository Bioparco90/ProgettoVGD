using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallManager : MonoBehaviour
{
    public float fireBallSpeed = 0;

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
        transform.LookAt(target);
    }



    void Update()
    {
        transform.LookAt(player.transform);
        rb.AddForce(rb.transform.forward * fireBallSpeed * Time.deltaTime, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "PlayerCollider")
        {
            player.takeDamage(20);
        }

        if (other.transform.name != "Enemy2")
        {
            Destroy(gameObject);
            print(other.transform.name);
        }
    }
}


