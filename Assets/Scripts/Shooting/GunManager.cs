using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using System;

public class GunManager : MonoBehaviour
{
    // Menu pausa
    public GameObject pauseMenuCanva;
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    private bool isPaused = false;

    public int selectedWeapon; //Variabile che contiene l'indice dell'arma selezionata in quel momento
    public TextMeshProUGUI ammmoCount;

    /*Creo delle variabili che contengono i valori da passare al costruttore della classe weapon quando 
      istanzierò gli oggetti per ogni arma*/

    string gunName = "Pistol";
    float gunFireRate = 0.5f; //Contiene il tempo che deve passare tra uno sparo e l'altro.
    int gunDamage = 15;
    Vector3 gunIdlePosition = new Vector3(1f, -0.2f, 0.5f);
    Vector3 gunAimPosition = new Vector3(0.75f, -0.30f, 0.7f);
    public ParticleSystem gunMuzzleFlash;
    public AudioSource gunShootSound;
    int maxGunAmmo = 40;
    int maxGunClipAmmo = 10;
    float gunReloadTime = 1f;
    Weapon gun;

    string machineGunName = "Machinegun";
    float machineGunFireRate = 0.1f; //Contiene il tempo che deve passare tra uno sparo e l'altro
    int machineGunDamage = 10;
    Vector3 machineGunIdlePosition = new Vector3(0.2f, -0.15f, 0.4f);
    Vector3 machineGunAimPosition = new Vector3(0f, -0.15f, 0.4f);
    public ParticleSystem machineGunMuzzleFlash;
    int maxMachineGunAmmo = 100;
    int maxMachineGunClipAmmo = 30;
    public AudioSource machineGunShootSound;
    float machineGunReloadTime = 1.3f;

    public AudioSource reloadSound;


    Weapon machineGun;

    string shotGunName = "Shotgun";
    float shotGunFireRate = 1.5f; //Contiene il tempo che deve passare tra uno sparo e l'altro
    Vector3 shotGunIdlePosition = new Vector3(0.3f, -0.2f, 0.5f);
    Vector3 shotGunAimPosition = new Vector3(0f, -0.2f, 0.3f);
    int shotGunDamage = 50;
    public ParticleSystem shotGunMuzzleFlash;
    public AudioSource shotGunShootSound;
    int maxShotGunAmmo = 40;
    int maxShotGunClipAmmo = 5;
    float shotgunReloadTime = 2f;

    Weapon shotGun;

    public List<Weapon> weaponList = new List<Weapon>(); //Lista di tutte le armi impugnate dal player*/
    float timeSinceLastShoot = 0; //Contiene il tempo trascorso dall'ulitmo sparo

    public Weapon activeWeapon;
    public Hud hud;

    private void Start()
    {
        bool isLoaded = PlayerPrefs.GetString("LoadedGame") == "True";

        /*Creazione delle singole armi*/
        gun = new Weapon(gunName, gunIdlePosition, gunAimPosition, gunFireRate, gunDamage, false, gunShootSound, reloadSound, maxGunAmmo, maxGunClipAmmo, gunReloadTime);
        machineGun = new Weapon(machineGunName, machineGunIdlePosition, machineGunAimPosition, machineGunFireRate, machineGunDamage, true, machineGunShootSound, reloadSound, maxMachineGunAmmo, maxMachineGunClipAmmo, machineGunReloadTime);
        shotGun = new Weapon(shotGunName, shotGunIdlePosition, shotGunAimPosition, shotGunFireRate, shotGunDamage, false, shotGunShootSound, reloadSound, maxShotGunAmmo, maxShotGunClipAmmo, shotgunReloadTime);


        /*Aggiunta delle armi alla lista*/
        weaponList.Add(gun);
        weaponList.Add(machineGun);
        weaponList.Add(shotGun);
        foreach (Weapon weapon in weaponList)
        {
            weapon.currentClipAmmo = weapon.maxClipAmmo;
        }

        if (isLoaded)
        {
            LoadAmmo();
            // PlayerPrefs.SetString("LoadedGame", "False");
        }

        selectedWeapon = 0;
        selectWeapon(); //Di default viene selezionata la prima arma della lista

        hud.UpdateAmmoCount();
        //ammmoCount.SetText(weaponList[selectedWeapon].currentClipAmmo + "/" + weaponList[selectedWeapon].maxAmmo);
    }

    private void Update()
    {
        timeSinceLastShoot += Time.deltaTime;

        if (activeWeapon != null && !isPaused)
        {
            weaponSwitch();
            hud.UpdateAmmoCount();
            //ammmoCount.SetText(activeWeapon.currentClipAmmo + "/" + activeWeapon.maxAmmo);
        }

        if (Input.GetKey(KeyCode.Mouse0) && activeWeapon != null && timeSinceLastShoot >= activeWeapon.fireRate && !activeWeapon.isReloading && activeWeapon.currentClipAmmo > 0 && !isPaused)
        {
            activeWeapon.shoot();
            timeSinceLastShoot = 0;
            hud.UpdateAmmoCount();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && activeWeapon != null && !activeWeapon.isAiming && !isPaused)
        {
            activeWeapon.startAim(this.gameObject, Camera.main);
        }

        if (Input.GetKeyUp(KeyCode.Mouse1) && activeWeapon != null && activeWeapon.isAiming && !isPaused)
        {
            activeWeapon.stopAim(this.gameObject, Camera.main);
        }

        if (Input.GetKey(KeyCode.R) && activeWeapon != null && activeWeapon.currentClipAmmo < activeWeapon.maxClipAmmo && activeWeapon.maxAmmo > 0 && !isPaused)
        {
            StartCoroutine(activeWeapon.reload());
            hud.UpdateAmmoCount();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;  // Mette il gioco in pausa
            pauseMenuCanva.SetActive(true);
        }
        else
        {
            activeWeapon.stopAim(this.gameObject, Camera.main);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;  // Riprende il gioco
            optionsMenu.SetActive(false); // Se abbiamo cliccato esc mentre siamo in options, così lo disattiva
            pauseMenu.SetActive(true); // Come sopra. Gestisce, nell'eventualità di una riapetrura del menu, la corretta visualizzazione 
            pauseMenuCanva.SetActive(false);
        }
    }


    /*Metodo che seleziona l'arma corretta*/
    void selectWeapon()
    {
        int i = 0;
        /*Itero per ogni elemento figlio del transform a cui verrà attaccato lo script
          quando trovo l'arma che sto cercando (indicata dalla variabile selectedWeapon) 
          attivo il GameObject dell'arma e imposto la posizione dell'arma nella scena
          
          Tutte le armi che non corrispondono a quella selezionata vengono disattivate*/
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
                transform.localPosition = weaponList[i].idlePosition;
                activeWeapon = weaponList[selectedWeapon];
                /* 
                 * TODO:
                 * inserire qui la visualizzazione a schermo dell'icona arma se possibile, oppure anche solo il nome 
                 */
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
        /**/
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
        hud.UpdateWeaponImage();
    }

    public void addAmmo(int ammoAmount)
    {
        activeWeapon.maxAmmo += ammoAmount;
    }

    public Weapon[] GetWeapons()
    {
        return weaponList.ToArray();
    }

    public void LoadAmmo()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        for (int i = 0; i < weaponList.Count; i++)
        {
            weaponList[i].maxAmmo = data.maxAmmo[i];
            weaponList[i].currentClipAmmo = data.currentClipAmmo[i];
        }
    }
}
