using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    /*Creo delle variabili che contengono i valori da passare al costruttore della classe weapon quando 
      istanzierò gli oggetti per ogni arma*/
    float gunFireRate; //Contiene il tempo che deve passare tra uno sparo e l'altro
    Vector3 gunIdlePosition=new Vector3 (1f,-0.6f,0.7f);
    Vector3 gunAimPosition=new Vector3 (-2f, 0.75f,-4f);
    Weapon gun;

    float machineGunFireRate; //Contiene il tempo che deve passare tra uno sparo e l'altro
    Vector3 machineGunIdlePosition=new Vector3 (0.5f,-0.2f,0.2f);
    Vector3 machineGunAimPosition=new Vector3 (-2f, 0.75f,-4f);
    Weapon machineGun;

    float shotGunFireRate; //Contiene il tempo che deve passare tra uno sparo e l'altro
    Vector3 shotGunIdlePosition=new Vector3 (1f,-0.6f,0.7f);
    Vector3 shotGunAimPosition=new Vector3 (-2f, 0.75f,-4f);
    Weapon shotGun;

    List<Weapon> weaponList= new List<Weapon>(); //Lista di tutte le armi impugnate dal player
    int selectedWeapon;
    private void Start() {
        gun=new Weapon (gunIdlePosition,gunAimPosition,gunFireRate,20);
        machineGun=new Weapon(machineGunIdlePosition, machineGunAimPosition,machineGunFireRate,10);
        shotGun=new Weapon (shotGunIdlePosition,shotGunAimPosition,shotGunFireRate,20);
        weaponList.Add(gun);
        weaponList.Add(machineGun);
        weaponList.Add(shotGun);
        selectWeapon();
    }

    private void Update() {
        int previousSelectedWeapon=selectedWeapon;
        if(Input.GetAxis("Mouse ScrollWheel") >=0f){
            if(selectedWeapon>=transform.childCount-1)
                selectedWeapon=0;
            else 
                selectedWeapon++;
        }

        if(Input.GetAxis("Mouse ScrollWheel") <= 0f){
            if(selectedWeapon <= 0)
                selectedWeapon=transform.childCount-1;
            else 
                selectedWeapon--;
        }
        if(selectedWeapon!=previousSelectedWeapon)
            selectWeapon();
    }

    void selectWeapon(){
        int i=0;
        foreach(Transform weapon in transform){
            if(i==selectedWeapon){
                weapon.gameObject.SetActive(true);
                transform.localPosition=weaponList[i].idlePosition;
            }
            else{
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
