using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float followSpeed;
    float targetInitialScale;
    Vector3 targetPos => target.position + offset * (target.localScale.x / targetInitialScale);

    void Start()
    {
        targetInitialScale = target.localScale.x;
        transform.position = targetPos;
    }

    void FixedUpdate()
    {
        var sizeMult = targetInitialScale / target.localScale.x; // increase follow speed when ship is smaller and vice versa
        transform.position = Vector3.Lerp(transform.position, targetPos, sizeMult * followSpeed * Time.fixedDeltaTime);
    }

    void Update()
    {
        // transform.localScale = Vector3.Lerp(transform.localScale, target.localScale, followSpeed * Time.fixedDeltaTime);
    }
}
