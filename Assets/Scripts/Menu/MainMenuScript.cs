using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public AudioSource music;
    private void Start()
    {
        Screen.fullScreen = PlayerPrefs.GetString("isFullscreen") == "True";
        float volume = PlayerPrefs.GetFloat("Volume");
        music.volume = GetNormalizedVolume(volume);
    }

    public void PlayGame()
    {
        PlayerPrefs.SetString("LoadedGame", "False");
        SceneManager.LoadScene(1);
    }

    public void SelectLevel(int scene)
    {
        PlayerPrefs.SetString("LoadedGame", "False");
        SceneManager.LoadScene(scene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadGame()
    {
        PlayerData playerData = SaveSystem.LoadPlayer();
        if (playerData != null)
        {
            PlayerPrefs.SetString("LoadedGame", "True");
            SceneManager.LoadScene(playerData.scene);
        }
    }

    private float GetNormalizedVolume(float volume)
    {
        return Mathf.InverseLerp(-80, 0, volume);
    }
}
