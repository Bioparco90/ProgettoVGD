using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    RaycastHit hitPoint;
    public Camera playerCamera;
    /*Creo delle variabili che contengono i valori da passare al costruttore della classe weapon quando 
      istanzierò gli oggetti per ogni arma*/
    float gunFireRate = 0.5f; //Contiene il tempo che deve passare tra uno sparo e l'altro.
    float gunDamage = 10.0f;
    Vector3 gunIdlePosition = new Vector3(1f, -0.4f, 0.5f);
    Vector3 gunAimPosition = new Vector3(0f, -0.25f, 1f);
    Weapon gun;

    float machineGunFireRate = 0.1f; //Contiene il tempo che deve passare tra uno sparo e l'altro
    float machineGunDamage = 7.0f;
    Vector3 machineGunIdlePosition = new Vector3(1f, -0.4f, 0.5f);
    Vector3 machineGunAimPosition = new Vector3(0f, -0.5f, 0.8f);
    Weapon machineGun;

    float shotGunFireRate = 1.5f; //Contiene il tempo che deve passare tra uno sparo e l'altro
    Vector3 shotGunIdlePosition = new Vector3(0.3f, -0.2f, 0.5f);
    Vector3 shotGunAimPosition = new Vector3(0f, -0.2f, 0.3f);
    float shotGunDamage = 50.0f;
    Weapon shotGun;

    List<Weapon> weaponList = new List<Weapon>(); //Lista di tutte le armi impugnate dal player

    float timeSinceLastShoot = 0; //Contiene il tempo trascorso dall'ulitmo sparo

    int selectedWeapon; //Variabile che contiene l'indice dell'arma selezionata in quel momento
    private void Start()
    {
        /*Creazione delle singole armi*/
        gun = new Weapon(gunIdlePosition, gunAimPosition, gunFireRate, gunDamage, false);
        machineGun = new Weapon(machineGunIdlePosition, machineGunAimPosition, machineGunFireRate, machineGunDamage, true);
        shotGun = new Weapon(shotGunIdlePosition, shotGunAimPosition, shotGunFireRate, shotGunDamage, false);

        /*Aggiunta delle armi alla lista*/
        weaponList.Add(gun);
        weaponList.Add(machineGun);
        weaponList.Add(shotGun);
        selectedWeapon = 0;
        selectWeapon(); //Di default viene selezionata la prima arma della lista

    }

    private void Update()
    {
        timeSinceLastShoot += Time.deltaTime;

        weaponSwitch();
        aim(weaponList[selectedWeapon], playerCamera);

        if (Input.GetKey(KeyCode.Mouse0) && timeSinceLastShoot >= weaponList[selectedWeapon].fireRate)
        {
            shoot(weaponList[selectedWeapon], playerCamera);
            timeSinceLastShoot = 0;
        }

    }




    /*Metodo che seleziona l'arma corretta*/
    void selectWeapon()
    {
        int i = 0;
        /*Itero per ogni elemento figlio del transform a cui verrà attaccato lo script
          quando trovo l'arma che sto cercando (indicata dalla variabile selectedWeapon) 
          attivo il GameObject dell'arma e imposto la posizione dell'arma nella scena
          
          Tutte le armi che non corrispondono a quella selezionata vengonon disattivate*/
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
                transform.localPosition = weaponList[i].idlePosition;
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }

    /*Metodo che switcha le armi tramite lo scroll della rotella del mouse*/
    void weaponSwitch()
    {
        int previousSelectedWeapon = selectedWeapon; //Salvo in una variabile l'arma precedentemente attiva

        if (Input.GetAxis("Mouse ScrollWheel") >= 0f)
        {
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        }

        if (Input.GetAxis("Mouse ScrollWheel") <= 0f)
        {
            if (selectedWeapon <= 0)
                selectedWeapon = transform.childCount - 1;
            else
                selectedWeapon--;
        }

        //Se l'arma attuale è diversa da quella precedente, allora faccio lo switch
        if (selectedWeapon != previousSelectedWeapon)
        {
            selectWeapon();
        }

    }

    void aim(Weapon activeWeapon, Camera playerCamera)
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && !activeWeapon.isAiming)
        {
            activeWeapon.isAiming = true;
            transform.localPosition = activeWeapon.aimPosition;
            playerCamera.fieldOfView = 50;
        }

        if (Input.GetKeyUp(KeyCode.Mouse1) && activeWeapon.isAiming)
        {
            activeWeapon.isAiming = false;
            transform.localPosition = activeWeapon.idlePosition;
            playerCamera.fieldOfView = 80;
        }
    }

    void shoot(Weapon activeWeapon, Camera playerCamera)
    {
        RaycastHit hitPoint;
        bool rayCastRes = Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hitPoint, Mathf.Infinity);
        if (rayCastRes)
        {
            /*print(hitPoint.transform.tag);
            print(activeWeapon.damage);*/
        }
    }
}
