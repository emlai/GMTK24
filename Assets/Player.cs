using UnityEngine;

public class Player : MonoBehaviour
{
    public float movementSpeed;
    public float rotationSpeed;
    [Range(0.01f, 1f)]
    public float scaleSpeed;
    Ship ship;

    void Start()
    {
        ship = GameObject.FindWithTag("Ship").GetComponent<Ship>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("UpDownThrust");
        var z = Input.GetAxis("Vertical");
        // transform.position += transform.rotation * (new Vector3(x, y, z) * movementSpeed * transform.localScale.x * Time.fixedDeltaTime);
        ship.transform.position += ship.transform.rotation * (new Vector3(-x, y, -z) * movementSpeed * Time.fixedDeltaTime);

        var mouseX = Input.GetAxis("Mouse X");
        var mouseY = Input.GetAxis("Mouse Y");
        transform.localEulerAngles += new Vector3(-mouseY, mouseX, 0) * rotationSpeed * Time.fixedDeltaTime;

        var scaleDiff = Input.GetAxis("Scale") * scaleSpeed;
        // Debug.Log($"{scaleDiff} {Mathf.Pow(scaleDiff, Time.fixedDeltaTime)}");
        // Camera.main.fieldOfView += scaleDiff * Time.fixedDeltaTime;
        ship.transform.localScale *= Mathf.Pow(1 + scaleDiff, Time.fixedDeltaTime);


        // var allObjects = FindObjectsByType<Transform>(FindObjectsSortMode.None);
        // foreach (var obj in allObjects)
        // {
        //     obj.localScale *= Mathf.Pow(1 - scaleDiff, Time.fixedDeltaTime);
        // }
    }
}
