using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // 目标对象，即角色的 Transform
    public float smoothSpeed = 0.125f;  // 摄像机跟随的平滑速度
    public Vector3 offset;  // 与目标之间的偏移量

    // 围墙的边界（请根据围墙的实际坐标设置）
    public Vector2 minBounds;
    public Vector2 maxBounds;

    private Camera cam; // 获取相机组件

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        
        float camHalfHeight = cam.orthographicSize;
        float camHalfWidth = cam.aspect * camHalfHeight;

        
        float clampedX = Mathf.Clamp(smoothedPosition.x, minBounds.x + camHalfWidth, maxBounds.x - camHalfWidth);
        float clampedY = Mathf.Clamp(smoothedPosition.y, minBounds.y + camHalfHeight, maxBounds.y - camHalfHeight);

       
        transform.position = new Vector3(clampedX, clampedY, smoothedPosition.z);
    }
}
