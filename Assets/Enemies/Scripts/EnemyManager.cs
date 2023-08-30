using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemyManager : MonoBehaviour
{
    int enemyHealt;

    private void Start()
    {
        enemyHealt = 0;
        switch (this.tag)
        {
            case "Enemy1":
                enemyHealt = 150;
                break;

            case "Enemy2":
                enemyHealt = 250;
                break;

            case "Enemy3":
                enemyHealt = 400;
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        print(this.name + " healt: " + enemyHealt);
    }
    public void takeDamage(int damageToTake)
    {
        if (enemyHealt - damageToTake <= 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            enemyHealt -= damageToTake;
        }
    }

}
