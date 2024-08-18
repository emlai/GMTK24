using UnityEngine;

public class Chaser : MonoBehaviour
{
    public GameObject targetToChase;
    public float triggerDistance = 100;
    public float chaseSpeed = 10;
    public float turnSpeed = 10;
    private bool chasing;

    private void FixedUpdate()
    {
        // Always rotate towards target
        var lookAtTarget = Quaternion.LookRotation(targetToChase.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookAtTarget, turnSpeed * Time.fixedDeltaTime);

        if (chasing)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetToChase.transform.position, chaseSpeed * Time.fixedDeltaTime);
        }
        else if (Vector3.Distance(transform.position, targetToChase.transform.position) < triggerDistance)
        {
            chasing = true;
        }
    }
}
