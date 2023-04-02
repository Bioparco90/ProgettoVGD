using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    private float mouseSensitivity=20.0f;
    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);
        
        transform.localRotation =Quaternion.Euler(xRotation, 0f,0f);
        player.Rotate(Vector3.up * mouseX);
    }
}
