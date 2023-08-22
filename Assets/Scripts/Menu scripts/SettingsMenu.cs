using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider masterVolumeSlider;
    public Dropdown resolutionDropdown;

    public Slider mouseSensivitySlider;
    public CameraController cameraController;

    public GunManager gunManager;
    public PlayerController player;
    public AudioSource pauseMenuAudio;

    Resolution[] resolutions;

    public void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<String> options = new List<String>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + " @ " + resolutions[i].refreshRate + "hz";
            options.Add(option);
            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume()
    {
        float volume = masterVolumeSlider.value;

        float normalizedVolume = GetNormalizedVolume(volume);

        audioMixer.SetFloat("masterVolume", volume);

        // Setta il volume dell'arma del giocatore
        gunManager.gunShootSound.volume = gunManager.machineGunShootSound.volume = gunManager.shotGunShootSound.volume = normalizedVolume;

        // Setta il volume della ricarica delle armi del giocatore
        gunManager.reloadSound.volume = normalizedVolume;

        player.takeDamageSound.volume = normalizedVolume;

        pauseMenuAudio.volume = normalizedVolume;

        PlayerPrefs.SetFloat("Volume", volume);
    }

    private float GetNormalizedVolume(float volume)
    {
        return Mathf.InverseLerp(-80, 0, volume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetString("isFullscreen", isFullscreen.ToString());
    }

    public void SetSensivity()
    {
        float sensivity = mouseSensivitySlider.value;
        cameraController.mouseSensitivity = sensivity;
        PlayerPrefs.SetFloat("Sensitivity", sensivity);
    }

}
