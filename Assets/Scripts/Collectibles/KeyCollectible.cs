using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollectible : MonoBehaviour
{
    BoxCollider doorCollider;
    Animator doorAnimator;
    private void Start()
    {
        doorAnimator = GameObject.FindGameObjectWithTag("Door").GetComponent<Animator>();
        doorCollider = GameObject.FindGameObjectWithTag("Door").GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerCollider")
        {
            doorAnimator.SetTrigger("OpenDoor");
            Destroy(this.gameObject);
            Destroy(doorCollider);
        }
    }

}
