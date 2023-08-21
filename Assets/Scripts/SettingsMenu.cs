using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider musicVolumeSlider;
    public Slider effectsVolumeSlider;
    public Slider voicesVolumeSlider;
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
        float musicVolume = musicVolumeSlider.value;
        float effectsVolume = effectsVolumeSlider.value;
        float voicesVolume = voicesVolumeSlider.value;

        float[] normalizedVolume = { GetNormalizedVolume(musicVolume), GetNormalizedVolume(effectsVolume), GetNormalizedVolume(voicesVolume) };

        audioMixer.SetFloat("musicVolume", musicVolume);
        audioMixer.SetFloat("effectsVolume", effectsVolume);
        audioMixer.SetFloat("voicesVolume", voicesVolume);

        // Setta il volume dell'arma del giocatore
        gunManager.gunShootSound.volume = gunManager.machineGunShootSound.volume = gunManager.shotGunShootSound.volume = normalizedVolume[1];

        // Setta il volume della ricarica delle armi del giocatore
        gunManager.reloadSound.volume = normalizedVolume[1];

        player.takeDamageSound.volume = normalizedVolume[2];

        pauseMenuAudio.volume = normalizedVolume[0];
    }

    private float GetNormalizedVolume(float volume)
    {
        return Mathf.InverseLerp(-80, 0, volume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetSensivity()
    {
        float sensivity = mouseSensivitySlider.value;
        cameraController.mouseSensitivity = sensivity;
    }

}
