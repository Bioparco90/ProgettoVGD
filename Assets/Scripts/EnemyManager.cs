using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int healt;
    int damage;
    void Start()
    {
        healt = 100;
        damage = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (healt <= 0)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    public void takeDamage(int damageToTake)
    {
        healt = healt - damageToTake <= 0 ? 0 : healt - damageToTake;
    }
}
