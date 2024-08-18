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
        if (chasing)
        {
            var lookAtTarget = Quaternion.LookRotation(transform.position - targetToChase.transform.position);
            // lookAtTarget.eulerAngles = new Vector3(lookAtTarget.eulerAngles.x, lookAtTarget.eulerAngles.y - 180, lookAtTarget.eulerAngles.z); // HACK: turn 90 degrees to fix the fish's forward axis.
            transform.rotation = Quaternion.Slerp(transform.rotation, lookAtTarget, turnSpeed * Time.fixedDeltaTime);
            transform.position = Vector3.MoveTowards(transform.position, targetToChase.transform.position, chaseSpeed * Time.fixedDeltaTime);
        }
        else if (Vector3.Distance(transform.position, targetToChase.transform.position) < triggerDistance)
        {
            chasing = true;
        }
    }
}
