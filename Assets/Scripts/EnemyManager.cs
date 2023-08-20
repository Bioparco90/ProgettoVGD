using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    bool canHit;
    PlayerController player;
    float timeSinceLastHit;
    float hitCooldown;
    public int healt;
    int damage;
    void Start()
    {
        timeSinceLastHit = 0;
        hitCooldown = 2;
        healt = 100;
        damage = 10;
        player = GameObject.FindGameObjectWithTag("PlayerCollider").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastHit += Time.deltaTime;
        if (timeSinceLastHit >= hitCooldown)
        {
            dealDamage();
        }

        if (healt <= 0)
        {
            GameObject.Destroy(this.gameObject);
        }
    }


    public void takeDamage(int damageToTake)
    {
        healt = healt - damageToTake <= 0 ? 0 : healt - damageToTake;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "PlayerCollider")
        {
            canHit = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlayerCollider")
        {
            canHit = false;
        }
    }

    public void dealDamage()
    {
        if (canHit)
        {
            player.takeDamage(damage);
            timeSinceLastHit = 0;
        }

    }
}
