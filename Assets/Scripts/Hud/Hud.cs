using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{

    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI healthText;
    public GunManager gunManager;
    public Image weaponImage;
    public Sprite[] sprites;
    public PlayerController player;

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
    }

    public void UpdateWeaponImage()
    {
        weaponImage.sprite = sprites[gunManager.selectedWeapon];
    }

    public void UpdateHealthText()
    {
        healthText.text = player.healtPoints.ToString();
    }
}
