using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool canMove;
    public GameObject player;
    Rigidbody rb;
    public float speed;
    private void Start() {
        rb=GetComponent<Rigidbody>(); 
        speed=10;
        canMove=false;
        player=GameObject.FindGameObjectWithTag("PlayerCollider");
    }
   

    void Update()
    {
        transform.LookAt(player.transform);
        
        if(canMove)
            rb.AddForce(speed*Time.deltaTime*transform.forward, ForceMode.Impulse);
    }
}
