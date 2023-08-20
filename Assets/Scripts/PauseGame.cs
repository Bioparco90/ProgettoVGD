using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    public GunManager gunManager;

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
        MainMenuScript.LoadGame();
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
