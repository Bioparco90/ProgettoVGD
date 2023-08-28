using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    int bossHealth;
    float testTimer;
    Animator bossAnimator;
    void Start()
    {
        testTimer = 0;
        bossHealth = 51;
        bossAnimator = GetComponent<Animator>();
    }


    void Update()
    {
        testTimer += Time.deltaTime;
        if (testTimer > 4)
            bossHealth = 40;
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
