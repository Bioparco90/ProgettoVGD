using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public TextMeshProUGUI ammoText;
    public GunManager gunManager;

    // Start is called before the first frame update
    void Start()
    {
        Test();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Test()
    {
        int ammo = gunManager.activeWeapon.currentClipAmmo;
        //int ammo = 15;
        ammoText.text = ammo.ToString();
        Debug.Log("Ammo: " + ammo);
    }
}
