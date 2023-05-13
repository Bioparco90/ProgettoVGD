using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon", order = 0)]

public class WeaponData : ScriptableObject {
    
    [Header ("Scene View")]
    public Vector3 idlePosition; //Posizione di base
    public Vector3 aimPosition;  //Posizione durante la mira
    //public ParticleSystem muzzleFlash;
    //public AudioSource shootSound;

    [Header ("Booleans")]
    public bool isAiming;  //True se il player sta mirando
    public bool canShoot;
    public bool isActive; //True se l'arma è utilizzata dal player in quel momento
    public bool isAutomatic;

    [Header ("Shooting and reloading")] 
    public float fireRate;  //Tempo che deve passare tra uno sparo e l'altro
    public float damage; //Danno inflitto da uno sparo dell'arma
    public int maxClipAmmo; //Munizioni massime per caricatore
    public int currentClipAmmo; //Munizioni attuali nel caricatore
    public int maxAmmo; //Munizioni massime dell'arma
    public float reloadTime;
}

