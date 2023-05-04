using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunManager : MonoBehaviour
{   bool isReloading=false;
    public TextMeshProUGUI ammmoCount;
    RaycastHit hitPoint;
    public Camera playerCamera;
    /*Creo delle variabili che contengono i valori da passare al costruttore della classe weapon quando 
      istanzierò gli oggetti per ogni arma*/
    float gunFireRate = 0.5f; //Contiene il tempo che deve passare tra uno sparo e l'altro.
    float gunDamage = 10.0f;
    Vector3 gunIdlePosition = new Vector3(1f, -0.4f, 0.5f);
    Vector3 gunAimPosition = new Vector3(0f, -0.25f, 1f);
    public ParticleSystem gunMuzzleFlash;
    public AudioSource gunShootSound;
    int maxGunAmmo=20;
    int maxGunClipAmmo=10;
    float gunReloadTime=1f;
    Weapon gun;

    float machineGunFireRate = 0.1f; //Contiene il tempo che deve passare tra uno sparo e l'altro
    float machineGunDamage = 7.0f;
    Vector3 machineGunIdlePosition = new Vector3(1f, -0.4f, 0.5f);
    Vector3 machineGunAimPosition = new Vector3(0f, -0.5f, 0.8f);
    public ParticleSystem machineGunMuzzleFlash;
    int maxMachineGunAmmo=50;
    int maxMachineGunClipAmmo=20;
    public AudioSource machineGunShootSound;
    float machineGunReloadTime=1.3f;

    public AudioSource reloadSound; 
    

    Weapon machineGun;

    float shotGunFireRate = 1.5f; //Contiene il tempo che deve passare tra uno sparo e l'altro
    Vector3 shotGunIdlePosition = new Vector3(0.3f, -0.2f, 0.5f);
    Vector3 shotGunAimPosition = new Vector3(0f, -0.2f, 0.3f);
    float shotGunDamage = 50.0f;
    public ParticleSystem shotGunMuzzleFlash;
    public AudioSource shotGunShootSound;
    int maxShotGunAmmo=20;
    int maxShotGunClipAmmo=5;
    float shotgunReloadTime=2f;

    Weapon shotGun;

    List<Weapon> weaponList = new List<Weapon>(); //Lista di tutte le armi impugnate dal player

    float timeSinceLastShoot = 0; //Contiene il tempo trascorso dall'ulitmo sparo

    int selectedWeapon; //Variabile che contiene l'indice dell'arma selezionata in quel momento
    private void Start()
    {
        /*Creazione delle singole armi*/
        gun = new Weapon(gunIdlePosition, gunAimPosition, gunFireRate, gunDamage, false, gunMuzzleFlash, gunShootSound,maxGunAmmo, maxGunClipAmmo, gunReloadTime);
        machineGun = new Weapon(machineGunIdlePosition, machineGunAimPosition, machineGunFireRate, machineGunDamage, true, machineGunMuzzleFlash, machineGunShootSound, maxMachineGunAmmo, maxMachineGunClipAmmo , machineGunReloadTime);
        shotGun = new Weapon(shotGunIdlePosition, shotGunAimPosition, shotGunFireRate, shotGunDamage, false, shotGunMuzzleFlash, shotGunShootSound,maxShotGunAmmo,maxShotGunClipAmmo,shotgunReloadTime);

        
        /*Aggiunta delle armi alla lista*/
        weaponList.Add(gun);
        weaponList.Add(machineGun);
        weaponList.Add(shotGun);
        foreach (Weapon weapon in weaponList){
            weapon.currentClipAmmo=weapon.maxClipAmmo;
        }
        

        selectedWeapon = 0;
        selectWeapon(); //Di default viene selezionata la prima arma della lista
        ammmoCount.SetText(weaponList[selectedWeapon].currentClipAmmo + "/" + weaponList[selectedWeapon].maxAmmo);

    }

    private void Update()
    {
        Weapon activeWeapon=weaponList[selectedWeapon];
        timeSinceLastShoot += Time.deltaTime;

        weaponSwitch();
        aim(activeWeapon, playerCamera);

        if (Input.GetKey(KeyCode.Mouse0) && timeSinceLastShoot >= activeWeapon.fireRate && !isReloading && activeWeapon.currentClipAmmo>0 )
        {
            if(activeWeapon.currentClipAmmo<=0){
                reload(activeWeapon);
            }
            shoot(activeWeapon, playerCamera);
            timeSinceLastShoot = 0;
        }
    
        if(Input.GetKey(KeyCode.R) && activeWeapon.currentClipAmmo<activeWeapon.maxClipAmmo && activeWeapon.maxAmmo>0){
            StartCoroutine(reload(activeWeapon));
        }
        ammmoCount.SetText(activeWeapon.currentClipAmmo + "/" + activeWeapon.maxAmmo);

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
            activeWeapon.currentClipAmmo--;
            activeWeapon.shootSound.Play();
            activeWeapon.muzzleFlash.Play();
            /*print(hitPoint.transform.tag);
            print(activeWeapon.damage);*/
        }
    }

    IEnumerator reload(Weapon activeWeapon){
        isReloading=true;
        //Se il caricatore ha almeno una munizione in meno rispetto al massimo trasportabile
        if(activeWeapon.currentClipAmmo<activeWeapon.maxClipAmmo && activeWeapon.maxAmmo>0){
            if(activeWeapon.maxAmmo<activeWeapon.maxClipAmmo-activeWeapon.currentClipAmmo){
                activeWeapon.currentClipAmmo=activeWeapon.maxAmmo;
                activeWeapon.maxAmmo=0;
            }
            else{
                activeWeapon.maxAmmo-=activeWeapon.maxClipAmmo-activeWeapon.currentClipAmmo; //Aggiorno la quantità di munizioni massime
                activeWeapon.currentClipAmmo=activeWeapon.maxClipAmmo; //Aggiorno la quantità di munizioni nel caricatore
            }
            
        }   
        reloadSound.Play();
        yield return new WaitForSeconds(activeWeapon.reloadTime);
        isReloading=false;
        
    }
}
