using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraAnimations : MonoBehaviour
{
      public Animator camAnim;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            camAnim.SetTrigger("walking");
        }
        else
        {
            camAnim.SetTrigger("idle");
        }
    }
}



   
       
