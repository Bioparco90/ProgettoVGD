using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuScript : MonoBehaviour
{
    public GunManager gunManager;

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

    public void Resume()
    {
        gunManager.TogglePause();
    }

    public void SaveGame()
    {
        throw new NotImplementedException();
    }

    public void LoadGame()
    {
        throw new NotImplementedException();
    }

    public void BackToStart()
    {
        SceneManager.LoadScene("Menu");
    }


    // Le due funzioni seguenti gestiscono il reset del focus dei bottoni (rimanevano selezionati anche dopo l'uscita dal menu)
    private void OnEnable()
    {
        // Quando il menu di pausa viene attivato, reimposta lo stato dei bottoni
        EventSystem.current.SetSelectedGameObject(null); // Rimuovi il focus da qualsiasi oggetto
    }

    private void OnDisable()
    {
        // Quando il menu di pausa viene disattivato, reimposta lo stato dei bottoni
        EventSystem.current.SetSelectedGameObject(null); // Rimuovi il focus da qualsiasi oggetto
    }
}
