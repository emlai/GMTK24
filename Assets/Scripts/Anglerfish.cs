using UnityEngine;

public class Anglerfish : MonoBehaviour
{
    void Start()
    {
        GetComponentInChildren<Animator>().Play("Swim");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().FreezeMovement();
            Camera.main.GetComponent<CameraController>().EnterFarAwayCameraMode();
            GetComponentInChildren<Animator>().Play("Bite");
            GetComponentInChildren<Animator>().speed = 2;
            Invoke(nameof(StopChasing), 1);
        }
    }

    void StopChasing()
    {
        GetComponent<Chaser>().enabled = false;
    }
}
