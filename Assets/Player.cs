using UnityEngine;

public class Player : MonoBehaviour
{
    public float movementSpeed;
    public float rotationSpeed;
    [Range(0.01f, 1f)]
    public float scaleSpeed;
    Ship ship;
    // Reticle reticle;
    private Animator animator;
    bool scaling;

    void Start()
    {
        ship = GameObject.FindWithTag("Ship").GetComponent<Ship>();
        animator = ship.GetComponentInChildren<Animator>();
        // reticle = GameObject.FindWithTag("Reticle").GetComponent<Reticle>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("UpDownThrust");
        var z = Input.GetAxis("Vertical");
        // transform.position += transform.rotation * (new Vector3(x, y, z) * movementSpeed * transform.localScale.x * Time.fixedDeltaTime);
        ship.transform.position += ship.transform.rotation * (new Vector3(x, y, z) * movementSpeed * transform.localScale.x * Time.fixedDeltaTime);
        if (z > 0)
        {
            animator.speed = 3;
        }
        else
        {
            animator.speed = 1;
        }

        var mouseX = Input.GetAxis("Mouse X");
        var mouseY = Input.GetAxis("Mouse Y");
        // transform.localEulerAngles += new Vector3(-mouseY, mouseX, 0) * rotationSpeed * Time.fixedDeltaTime;
        ship.transform.localEulerAngles += new Vector3(-mouseY, mouseX, 0) * rotationSpeed * Time.fixedDeltaTime;

        // var scaleDiff = Input.GetAxis("Scale") * scaleSpeed;
        // Debug.Log($"{scaleDiff} {Mathf.Pow(scaleDiff, Time.fixedDeltaTime)}");
        // Camera.main.fieldOfView += scaleDiff * Time.fixedDeltaTime;
        // ship.transform.localScale *= Mathf.Pow(1 + scaleDiff, Time.fixedDeltaTime);


        // var allObjects = FindObjectsByType<Transform>(FindObjectsSortMode.None);
        // foreach (var obj in allObjects)
        // {
        //     obj.localScale *= Mathf.Pow(1 - scaleDiff, Time.fixedDeltaTime);
        // }
        // Debug.DrawRay(ship.transform.position, ship.transform.forward * 10000, Color.red, 1, false);
    }

    // void Update()
    // {
    //     // if (reticle.raycastHit != null)
    //     // {
    //     //     var target = reticle.raycastHit.Value.collider.gameObject;
    //     //
    //     //     if (Input.GetButtonDown("ScaleUp"))
    //     //     {
    //     //         target.transform.DOScale(target.transform.localScale * 2f, 1);
    //     //     }
    //     //     else if (Input.GetButtonDown("ScaleDown"))
    //     //     {
    //     //         target.transform.DOScale(target.transform.localScale * 0.5f, 1);
    //     //     }
    //     // }
    //
    //     if (!scaling)
    //     {
    //         if (Input.GetButtonDown("ScaleUp"))
    //         {
    //             scaling = true;
    //             ship.transform.DOScale(ship.transform.localScale * 2f, 0.5f).OnComplete(() => scaling = false);
    //         }
    //         else if (Input.GetButtonDown("ScaleDown"))
    //         {
    //             scaling = true;
    //             ship.transform.DOScale(ship.transform.localScale * 0.5f, 0.5f).OnComplete(() => scaling = false);
    //         }
    //     }
    // }
}
