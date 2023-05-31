using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCollectible : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            print("Ammo sta toccando il player");

            GunManager gunManager = GameObject.Find("WeaponHolder").GetComponent<GunManager>();

            gunManager.addAmmo(10);

            GameObject.Destroy(this.gameObject);

        }

    }
}
