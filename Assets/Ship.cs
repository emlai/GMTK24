using DG.Tweening;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public float forwardMoveSpeed;
    Player player;
    bool growing;

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
        if (z >= 0) // Don't auto-move ship forward when player is manually moving backwards.
        {
            // Auto-move forward
            var diff = transform.forward * forwardMoveSpeed * Time.fixedDeltaTime;
            transform.position += diff;
        }
        // player.transform.position += diff;
    }

    public void Grow()
    {
        if (growing) return;
        growing = true;
        transform.DOScale(transform.localScale * 2, 0.2f).SetEase(Ease.InOutBack).OnComplete(() => growing = false);
    }
}
