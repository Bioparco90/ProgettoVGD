using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Weapon
{
    public Vector3 idlePosition; //Posizione di base
    public Vector3 aimPosition { get; set; }  //Posizione durante la mira
    public bool isReloading;
    public bool isAiming { get; set; } //True se il player sta mirando
    public bool canShoot { get; set; }
    public float fireRate { get; set; } //Tempo che deve passare tra uno sparo e l'altro
    public int damage { get; set; } //Danno inflitto da uno sparo dell'arma
    public bool isActive; //True se l'arma è utilizzata dal player in quel momento
    public bool isAutomatic;
    public ParticleSystem muzzleFlash;
    public AudioSource shootSound;
    public AudioSource reloadSound;
    public int maxClipAmmo; //Munizioni massime per caricatore
    public int currentClipAmmo; //Munizioni attuali nel caricatore
    public int maxAmmo; //Munizioni massime dell'arma
    public float reloadTime;

    public Weapon(Vector3 idlePosition, Vector3 aimPosition, float fireRate, int damage, bool isAutomatic, ParticleSystem muzzleFlash, AudioSource shootSound, AudioSource reloadSound, int maxAmmo, int maxClipAmmo, float reloadTime)
    {
        this.idlePosition = idlePosition;
        this.aimPosition = aimPosition;
        this.fireRate = fireRate;
        this.damage = damage;
        this.isAutomatic = isAutomatic;
        this.muzzleFlash = muzzleFlash;
        this.shootSound = shootSound;
        this.maxAmmo = maxAmmo;
        this.maxClipAmmo = maxClipAmmo;
        this.reloadTime = reloadTime;
        this.reloadSound = reloadSound;
    }

    public void shoot(Camera playerCamera)
    {
        EnemyManager enemy;
        RaycastHit hitPoint;
        bool rayCastRes = Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hitPoint, Mathf.Infinity);
        if (rayCastRes)
        {
            if (hitPoint.transform.GetComponent<EnemyManager>() != null)
            {
                enemy = hitPoint.transform.GetComponent<EnemyManager>();
                enemy.takeDamage(this.damage);

            }
            Debug.Log(hitPoint.transform.name);
            currentClipAmmo--;
            shootSound.Play();
            muzzleFlash.Play();
        }
    }

    public void startAim(GameObject weaponHolder, Camera playerCamera)
    {
        isAiming = true;
        weaponHolder.transform.localPosition = aimPosition;
        playerCamera.fieldOfView = 50;
    }
    public void stopAim(GameObject weaponHolder, Camera playerCamera)
    {
        isAiming = false;
        weaponHolder.transform.localPosition = idlePosition;
        playerCamera.fieldOfView = 80;
    }

    public IEnumerator reload()
    {
        isReloading = true;
        //Se il caricatore ha almeno una munizione in meno rispetto al massimo trasportabile
        if (currentClipAmmo < maxClipAmmo && maxAmmo > 0)
        {
            if (maxAmmo < maxClipAmmo - currentClipAmmo)
            {
                currentClipAmmo = maxAmmo;
                maxAmmo = 0;
            }
            else
            {
                maxAmmo -= maxClipAmmo - currentClipAmmo; //Aggiorno la quantità di munizioni massime
                currentClipAmmo = maxClipAmmo; //Aggiorno la quantità di munizioni nel caricatore
            }

        }
        reloadSound.Play();
        yield return new WaitForSeconds(reloadTime);
        isReloading = false;

    }
}
