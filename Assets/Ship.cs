using UnityEngine;

public class Ship : MonoBehaviour
{
    public float forwardMoveSpeed;
    Player player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.R))
        {
            transform.position = Vector3.zero;
        }

        var z = Input.GetAxis("Vertical");
        if (z == 0) // Don't auto-move ship forward when player is manually moving backwards.
        {
            var diff = Vector3.forward * forwardMoveSpeed * Time.fixedDeltaTime;
            transform.position += diff;
        }
        // player.transform.position += diff;
    }
}
