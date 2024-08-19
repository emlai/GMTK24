using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float followSpeed;
    public float rotationSpeed = 1;
    Vector3 targetPos => target.position + target.rotation * offset;
    // private Vector3 targetPos => target.position + target.rotation * (offset * (target.localScale.x));

    void Start()
    {
        transform.position = targetPos;
    }

    void FixedUpdate()
    {
        // var sizeMult = 1 / target.localScale.x; // increase follow speed when ship is smaller and vice versa
        transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.fixedDeltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, rotationSpeed * Time.fixedDeltaTime);
    }
}
