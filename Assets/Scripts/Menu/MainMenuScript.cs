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
        SceneManager.LoadScene(1);
    }

    public void SelectLevel(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public static void LoadGame()
    {
        throw new NotImplementedException();
    }

    private float GetNormalizedVolume(float volume)
    {
        return Mathf.InverseLerp(-80, 0, volume);
    }
}
