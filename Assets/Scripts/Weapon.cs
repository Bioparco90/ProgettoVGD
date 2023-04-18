using UnityEngine;

public class Weapon : MonoBehaviour
{
    private string weaponName;
    private int damage;
    private float fireRate; // colpi al secondo
    private float lastFiredTime;


    public Weapon(string weaponName, int damage, float fireRate)
    {
        this.weaponName = weaponName;
        this.damage = damage;
        this.fireRate = fireRate;
        lastFiredTime = float.MinValue;
    }

    public string GetName()
    {
        return weaponName;
    }

    public int GetDamage()
    {
        return damage;
    }

    public float GetFireRate()
    {
        return fireRate;
    }

    public void SetName(string name)
    {
        this.weaponName = name;
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    public void SetFireRate(float fireRate)
    {
        this.fireRate = fireRate;
    }

    public void Fire()
    {
        /* 
         * Prendiamo il tempo trascorso dall'inizio del gioco.
         * Lo confrontiamo con lastFiredTime + (1 / fireRate), che
         * rappresenta il tempo in cui l'arma sarebbe in grado 
         * di sparare di nuovo se sparasse alla massima velocità di fuoco
         * (fireRate colpi al secondo). Se il tempo attuale è inferiore
         * a questo valore sommato al tempo dell'ultimo colpo sparato, 
         * allora non è ancora passato abbastanza tempo per sparare di nuovo
         */
        float currentTime = Time.time; 
        if (currentTime > lastFiredTime + (1 / fireRate)) 
        {
            return;
        }

        /* 
         * inserire qui il codice per gestire lo sparo 
         */

        lastFiredTime = currentTime;
    }
}