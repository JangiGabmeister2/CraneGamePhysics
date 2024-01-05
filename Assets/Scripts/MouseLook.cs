using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Header("Camera Look")]
    public Vector2 sensitivity = new Vector2(50, 50);
    [SerializeField] Vector2 _viewClampX = new Vector2(-60, 60);
    [SerializeField] Vector2 _viewClampY = new Vector2(135, 215);

    float xRotation;
    float yRotation;

    Camera cam => GetComponent<Camera>();

    private void Update()
    {
        MoveCamera();
        ZoomCamera();
    }

    private void MoveCamera()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        float mouseX = Input.GetAxisRaw("Mouse X") * sensitivity.x * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensitivity.y * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, _viewClampX.x, _viewClampX.y);
        yRotation = Mathf.Clamp(yRotation, _viewClampY.x, _viewClampY.y);

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
    }

    private void ZoomCamera()
    {
        float fov = cam.fieldOfView;

        if (Input.mouseScrollDelta.normalized.y > 0)
        {
            fov -= 5;

            if (fov < 20)
            {
                fov = 20;
            }
        }
        else if (Input.mouseScrollDelta.normalized.y < 0)
        {
            fov += 5;

            if (fov > 150)
            {
                fov = 150;
            }
        }

        cam.fieldOfView = fov;
    }
}