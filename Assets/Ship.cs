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

        var diff = Vector3.forward * forwardMoveSpeed * Time.fixedDeltaTime;
        transform.position += diff;
        // player.transform.position += diff;
    }
}
