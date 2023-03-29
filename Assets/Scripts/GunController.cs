using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunController : MonoBehaviour
{
    RaycastHit hitPoint;
    void Start()
    {

    }
    
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            shoot();
        }
    }

    private void shoot()
    {
        bool rayCastRes = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out (hitPoint), Mathf.Infinity);

        if (rayCastRes)
        {
            print(hitPoint.collider.gameObject.tag);
        }
    }
}
