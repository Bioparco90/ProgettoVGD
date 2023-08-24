using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    Transform playerTransform;
    Vector3 levelOneStartPos = new Vector3(-30, 1, -7);
    Vector3 levelTwoStartPos = new Vector3(26, 1, -265);
    Vector3 levelThreeStartPos = new Vector3(-905, 1, -1040);

    public Toggle isFullScreenToggle;
    public Toggle isImmortal;
    public Slider sensitivitySlider;
    public Slider volumeSlider;
    public AudioMixer audioMixer;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("PlayerCollider").GetComponent<Transform>();
        switch (SceneManager.GetActiveScene().name)
        {
            case "L1":
                playerTransform.position = levelOneStartPos;
                break;

            case "L2":
                playerTransform.position = levelTwoStartPos;
                break;

            case "L3":
                playerTransform.position = levelThreeStartPos;
                break;
        }

        SetSettings();
    }

    private void SetSettings()
    {
        audioMixer.SetFloat("masterVolume", PlayerPrefs.GetFloat("Volume"));
        volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        isFullScreenToggle.isOn = PlayerPrefs.HasKey("isFullscreen") ? PlayerPrefs.GetString("isFullscreen") == "True" : true;
        sensitivitySlider.value = PlayerPrefs.HasKey("Sensitivity") ? PlayerPrefs.GetFloat("Sensitivity") : 150f;
        isImmortal.isOn = PlayerPrefs.HasKey("isImmortal") ? PlayerPrefs.GetString("isImmortal") == "True" : false;
    }
}
