using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float[] position;
    public float[] rotation;
    public int health;
    public string scene;
    public int[] maxAmmo;
    public int[] currentClipAmmo;
    
    public PlayerData(PlayerController player, GunManager guns)
    {
        scene = player.scene;

        health = player.healtPoints;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

        Weapon[] weapons = guns.GetWeapons();

        maxAmmo = new int[weapons.Length];
        currentClipAmmo = new int[weapons.Length];
        for (int i = 0; i < weapons.Length; i++)
        {
            maxAmmo[i] = weapons[i].maxAmmo;
            currentClipAmmo[i] = weapons[i].currentClipAmmo;
        }
    }
}
