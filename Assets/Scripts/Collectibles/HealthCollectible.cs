using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            PlayerController Player = other.transform.GetComponent<PlayerController>();

            if (Player.healtPoints < 100)
            {
                Player.healPlayer(10);

                GameObject.Destroy(this.gameObject);
            }
        }
    }
}
