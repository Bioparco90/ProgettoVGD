using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{

    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI healthText;
    public GunManager gunManager;
    public Image weaponImage;
    public Sprite[] sprites;
    public PlayerController player;

    private BossManager bossObject;
    private TextMeshProUGUI bossHealthText;

    private void Awake()
    {
        // Verifica se sei nella scena "Lboss" e poi carica il GameObject "Boss"
        if (SceneManager.GetActiveScene().name == "LBoss")
        {
            bossObject = FindObjectOfType<BossManager>(); // Assumi che il GameObject abbia il nome "Boss"
            if (bossObject == null)
            {
                Debug.LogWarning("Boss GameObject not found in scene 'Lboss'. Make sure to assign it manually.");
            }
            GameObject bossHealthObject = GameObject.FindGameObjectWithTag("BossHealthCounter");
            if (bossHealthObject != null)
            {
                bossHealthText = bossHealthObject.GetComponent<TextMeshProUGUI>();
            }
        }
    }

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

    public void UpdateHealthBossText()
    {
        bossHealthText.text = bossObject.bossHealth.ToString();
    }
}
