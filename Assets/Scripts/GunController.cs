using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunController : MonoBehaviour
{
    void Start()
    {
        
    }
    
    void FixedUpdate()
    {
        RaycastHit hitPoint;
        bool rayCastRes = Physics.Raycast(transform.position, transform.forward,out(hitPoint), Mathf.Infinity);

        if (rayCastRes)
            Debug.DrawLine(transform.position, transform.forward, Color.red);
    }
}
