using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool canMove;
    public GameObject target;
    Rigidbody rb;
    public float speed;
    private void Start() {
        rb=GetComponent<Rigidbody>(); 
        speed=10;
        canMove=false;
        target=GameObject.FindGameObjectWithTag("PlayerCollider");
    }
   

    void Update()
    {
        transform.LookAt(target.transform);
        
        if(canMove)
            rb.AddForce(speed*Time.deltaTime*transform.forward, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.name=="Player"){
            PlayerController Player=other.transform.GetComponent<PlayerController>();
            Player.takeDamage(10);
        }
    }
}
