using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunController : MonoBehaviour
{
    private Vector3 gunIdlePosition=new Vector3 (-0.8f,0.5f,-4f);
    private Vector3 gunAimPosition=new Vector3 (-2f, 0.5f,-4f);
    int aimingFOV=50;
    int idleFOV=80;
    public float zoomSpeed=2;
    bool isAiming=false;
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

        if(!isAiming && Input.GetKeyDown(KeyCode.Mouse1)){
            gun.localPosition=gunAimPosition;
            isAiming=true;
            Camera.main.fieldOfView=Mathf.MoveTowards(aimingFOV, idleFOV, zoomSpeed * Time.deltaTime);
        }
        
        if(isAiming && Input.GetKeyUp(KeyCode.Mouse1)){
            gun.localPosition=gunIdlePosition;
            isAiming=false;
            Camera.main.fieldOfView=Mathf.MoveTowards(idleFOV, aimingFOV, zoomSpeed * Time.deltaTime);
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
