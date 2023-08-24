using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    int bossHealth;
    Animator bossAnimator;
    void Start()
    {
        bossHealth = 100;
        bossAnimator = GetComponent<Animator>();
    }


    void Update()
    {
        print(this.name + " healt: " + bossHealth);
        bossAnimator.SetInteger("bossHealth", bossHealth);
    }

    public void takeDamage(int damageToTake)
    {
        if (bossHealth - damageToTake <= 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            bossHealth -= damageToTake;
        }
    }
}
