using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCollectible : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            GunManager gunManager = GameObject.Find("WeaponHolder").GetComponent<GunManager>();

            gunManager.addAmmo(50);

            GameObject.Destroy(this.gameObject);
        }

    }
}
