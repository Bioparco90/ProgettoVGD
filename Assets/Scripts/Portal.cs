using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public void NextLevel()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextScene);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "PlayerCollider")
        {
            NextLevel();
        }
    }
}
