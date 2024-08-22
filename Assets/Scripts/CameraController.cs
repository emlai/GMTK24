using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float followSpeed;
    public float rotationSpeed = 1;
    bool farAwayCameraMode;
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
    // private Vector3 targetPos => target.position + target.rotation * (offset * (target.localScale.x));

    void Start()
    {
        transform.position = targetPos;
    }

    void FixedUpdate()
    {
        // var sizeMult = 1 / target.localScale.x; // increase follow speed when ship is smaller and vice versa
        if (farAwayCameraMode)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.fixedDeltaTime * 0.1f);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.fixedDeltaTime);
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, rotationSpeed * Time.fixedDeltaTime);
    }

    public void EnterFarAwayCameraMode()
    {
        farAwayCameraMode = true;
    }
}
