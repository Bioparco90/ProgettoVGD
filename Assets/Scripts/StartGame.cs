using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    Transform playerTransform;
    Vector3 levelOneStartPos = new Vector3(-30, 1, -7);
    Vector3 levelTwoStartPos = new Vector3(26, 1, -265);
    Vector3 levelThreeStartPos = new Vector3(-905, 1, -1040);

    public Toggle isFullScreenToggle;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("PlayerCollider").GetComponent<Transform>();
        switch (SceneManager.GetActiveScene().name)
        {
            case "L1":
                playerTransform.position = levelOneStartPos;
                break;

            case "L2":
                playerTransform.position = levelTwoStartPos;
                break;

            case "L3":
                playerTransform.position = levelThreeStartPos;
                break;
        }

        isFullScreenToggle.isOn = PlayerPrefs.GetString("isFullscreen") == "True";

    }
}
