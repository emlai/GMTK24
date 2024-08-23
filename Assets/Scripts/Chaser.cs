using UnityEngine;

public class Chaser : MonoBehaviour
{
    GameObject targetToChase;
    public float triggerDistance = 100;
    public float chaseSpeed = 10;
    public float turnSpeed = 10;
    private bool chasing;
    CharacterController characterController;

    private void Start()
    {
        targetToChase = GameObject.FindGameObjectWithTag("Player");
        characterController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        var forward = targetToChase.transform.position - transform.position;
        if (forward != Vector3.zero)
        {
            var lookAtTarget = Quaternion.LookRotation(forward, targetToChase.transform.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookAtTarget, turnSpeed * Time.fixedDeltaTime);
        }

        if (chasing)
        {
            characterController.Move(forward.normalized * chaseSpeed * Time.fixedDeltaTime);
        }
        else if (Vector3.Distance(transform.position, targetToChase.transform.position) < triggerDistance)
        {
            chasing = true;

            var audioSource = GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
    }
}
