using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public AudioSource music;
    // Start is called before the first frame update
    void Start()
    {
        float volume = PlayerPrefs.GetFloat("Volume");
        music.volume = GetNormalizedVolume(volume);
        StartCoroutine(WaitAndLoadMainMenu());
    }

    IEnumerator WaitAndLoadMainMenu()
    {
        // Attendi per 5 secondi
        yield return new WaitForSeconds(5f);

        // Carica la scena del menu principale
        SceneManager.LoadScene(0);
    }

    private float GetNormalizedVolume(float volume)
    {
        return Mathf.InverseLerp(-80, 0, volume);
    }
}
