using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunController : MonoBehaviour
{
    Vector3 gunIdlePosition=new Vector3 (-0.8f,0.5f,-4f);
    public Transform gun;
    RaycastHit hitPoint;
    void Start()
    {
        gun.localPosition=gunIdlePosition;
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
