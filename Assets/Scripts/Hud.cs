using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hud : MonoBehaviour
{

    public TextMeshProUGUI ammoText;
    public GunManager gunManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateAmmoCount()
    {
        string currentClipAmmo = gunManager.activeWeapon.currentClipAmmo.ToString();
        string maxAmmo = gunManager.activeWeapon.maxAmmo.ToString();
        ammoText.text = currentClipAmmo + "/" + maxAmmo;
        Debug.Log("Ammo: " + ammoText.text);
    }
}
