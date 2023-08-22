using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    private void Start()
    {
        Screen.fullScreen = PlayerPrefs.GetString("isFullscreen") == "True";
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
}
