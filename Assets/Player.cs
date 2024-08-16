using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movementSpeed;
    public float rotationSpeed;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("UpDownThrust");
        var z = Input.GetAxis("Vertical");
        transform.position += transform.rotation * (new Vector3(x, y, z) * movementSpeed * Time.fixedDeltaTime);

        var mouseX = Input.GetAxis("Mouse X");
        var mouseY = Input.GetAxis("Mouse Y");
        transform.localEulerAngles += new Vector3(-mouseY, mouseX, 0) * rotationSpeed * Time.fixedDeltaTime;
    }
}
