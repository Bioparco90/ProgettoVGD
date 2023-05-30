using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.name=="Player"){
            PlayerController Player=other.transform.GetComponent<PlayerController>();
            Player.healtPoints+=10;
        }
    }
}
