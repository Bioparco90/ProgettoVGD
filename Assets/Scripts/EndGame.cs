using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitAndLoadMainMenu());
    }

    IEnumerator WaitAndLoadMainMenu()
    {
        // Attendi per 5 secondi
        yield return new WaitForSeconds(5f);

        // Carica la scena del menu principale
        SceneManager.LoadScene(0);
    }
}
