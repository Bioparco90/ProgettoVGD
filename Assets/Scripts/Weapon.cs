using UnityEngine;

public class Weapon
{
    public Vector3 idlePosition; //Posizione di base
    public Vector3 aimPosition { get; set; }  //Posizione durante la mira
    public bool isAiming { get; set; } //True se il player sta mirando
    public bool canShoot { get; set; }
    public float fireRate { get; set; } //Tempo che deve passare tra uno sparo e l'altro
    public float damage { get; set; } //Danno inflitto da uno sparo dell'arma
    public bool isActive; //True se l'arma è utilizzata dal player in quel momento
    public bool isAutomatic;
    public ParticleSystem muzzleFlash;
    public AudioSource shootSound;
    public int maxClipAmmo; //Munizioni massime per caricatore
    public int currentClipAmmo; //Munizioni attuali nel caricatore
    public int maxAmmo; //Munizioni massime dell'arma
    public float reloadTime;

    public Weapon(Vector3 idlePosition, Vector3 aimPosition, float fireRate, float damage, bool isAutomatic, ParticleSystem muzzleFlash, AudioSource shootSound, int maxAmmo, int maxClipAmmo, float reloadTime)
    {
        this.idlePosition = idlePosition;
        this.aimPosition = aimPosition;
        this.fireRate = fireRate;
        this.damage = damage;
        this.isAutomatic = isAutomatic;
        this.muzzleFlash=muzzleFlash;
        this.shootSound=shootSound;
        this.maxAmmo=maxAmmo;
        this.maxClipAmmo=maxClipAmmo;
        this.reloadTime=reloadTime;
    }
}
