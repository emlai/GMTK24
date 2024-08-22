using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float followSpeed;
    public float rotationSpeed = 1;
    internal bool farAwayCameraMode;
    Vector3 targetPos
    {
        get
        {
            var offset = this.offset;
            if (farAwayCameraMode)
            {
                offset.y = 0;
                offset *= 10;
            }

            return target.position + target.rotation * offset;
        }
    }

    void Start()
    {
        transform.position = targetPos;
    }

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.fixedDeltaTime * (farAwayCameraMode ? 0.5f : 1));
        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, rotationSpeed * Time.fixedDeltaTime);
    }
}
