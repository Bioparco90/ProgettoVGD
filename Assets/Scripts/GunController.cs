using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunController : MonoBehaviour
{
    bool isAiming=false;
    bool canShoot=true;
    int aimingFOV=50;
    int idleFOV=80;
    public float zoomSpeed=2;
    float shootingTimer; //Tiene conto di quanto tempo è passato dall'ultimo sparo
    float fireRate; //Contiene il tempo che deve passare tra uno sparo e l'altro

    public Transform bulletSpawn;
    public GameObject bulletPrefab;
    public Rigidbody bulletRB;
    public Transform gun;
    RaycastHit hitPoint;

    private Vector3 gunIdlePosition=new Vector3 (-0.8f,0.5f,-4f);
    private Vector3 gunAimPosition=new Vector3 (-2f, 0.5f,-4f);
    private Vector3 bulletSpwanPosition= new Vector3 (0.5f,-0.822f,1.7f);
    void Start()
    {
        gun.localPosition=gunIdlePosition;
        bulletSpawn.transform.localPosition=bulletSpwanPosition;
        bulletRB=bulletPrefab.GetComponent<Rigidbody>();
        fireRate=1.0f;
        shootingTimer=fireRate;
        
    }
    
    void FixedUpdate()
    {
        shootingTimer-=Time.deltaTime;
        if(shootingTimer<=0){
            shootingTimer=fireRate;
            canShoot=true;
        }
            
        if (Input.GetKeyDown(KeyCode.Mouse0) && canShoot)
        {
            shoot();
            canShoot=false;
            shootingTimer=fireRate;
        }

        //If che gestisce la mira
        //Cambia la posizione della pistola (mettendola al centro) e aumenta lo zoom della camera
        if(!isAiming && Input.GetKeyDown(KeyCode.Mouse1)){
            isAiming=true;
            gun.localPosition=gunAimPosition;
            Camera.main.fieldOfView=Mathf.MoveTowards(aimingFOV, idleFOV, zoomSpeed * Time.deltaTime); //Diminuisco il FOV della camera per zoomare
        }

        //If che gestice il momento in cui il player smette di mirare
        //Rimette la pistola nella posizione iniziale e ripristina il FOV 
        if(isAiming && Input.GetKeyUp(KeyCode.Mouse1)){
            isAiming=false;
            gun.localPosition=gunIdlePosition;
            Camera.main.fieldOfView=Mathf.MoveTowards(idleFOV, aimingFOV, zoomSpeed * Time.deltaTime);
        }
    }
    
    private void shoot()
    {
        bool rayCastRes = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out (hitPoint), Mathf.Infinity);

        if (rayCastRes)
        {
            print(hitPoint.collider.gameObject.tag);
            Instantiate(bulletPrefab,bulletSpawn.transform);
            //bulletRB.AddForce(Vector3.forward,ForceMode.Impulse);
        }
    }
}
