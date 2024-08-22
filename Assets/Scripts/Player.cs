using System;
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
    public ParticleSystem boostParticles;
    [SerializeField] PauseMenu pauseMenu;
    [NonSerialized] public bool movementFrozen;

    void Awake()
	{
		rotationSpeed = pauseMenu.mouseSensitivity;
	}


	public void SetSensitivity()
	{
		rotationSpeed = PlayerPrefs.GetFloat("MouseSensitivity", 300);
	}

	void Start()
    {
        SetSensitivity();
		ship = GameObject.FindWithTag("Ship").GetComponent<Ship>();
        animator = ship.GetComponentInChildren<Animator>();
        // reticle = GameObject.FindWithTag("Reticle").GetComponent<Reticle>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
		rotationSpeed = pauseMenu.mouseSensitivity;
	}

    void FixedUpdate()
    {
        if (!movementFrozen)
        {
            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("UpDownThrust");
            var z = Input.GetAxis("Vertical");
            ship.transform.position += ship.transform.rotation * (new Vector3(x, y, z) * movementSpeed * transform.localScale.x * Time.fixedDeltaTime);
            if (z > 0)
            {
                animator.speed = 3;
            }
            else
            {
                animator.speed = 1;
            }
        }

        var mouseX = Input.GetAxis("Mouse X");
        var mouseY = Input.GetAxis("Mouse Y");
        ship.transform.Rotate(new Vector3(-mouseY, mouseX, 0) * rotationSpeed * Time.fixedDeltaTime);
    }

    void Update()
    {
        if (movementFrozen) return;

        if (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") > 0)
        {
            boostParticles.Stop();
            boostParticles.Play();
        }

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
    }

    public void FreezeMovement()
    {
        movementFrozen = true;
    }
}
