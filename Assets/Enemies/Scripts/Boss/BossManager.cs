using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BossManager : MonoBehaviour
{
    int bossHealth;
    float testTimer;
    Animator bossAnimator;
    void Start()
    {
        testTimer = 0;
        bossHealth = 1500;
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
            SceneManager.LoadScene(5);
        }
        else
        {
            bossHealth -= damageToTake;
        }
    }
}
