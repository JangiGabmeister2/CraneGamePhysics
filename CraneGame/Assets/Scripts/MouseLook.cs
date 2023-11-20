using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Header("Camera Look")]
    [SerializeField] Vector2 _sensitivity = new Vector2(50, 50);
    [SerializeField] Vector2 _viewClampX = new Vector2(-60, 60);
    [SerializeField] Vector2 _viewClampY = new Vector2(135, 215);

    float xRotation;
    float yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * _sensitivity.x;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * _sensitivity.y;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, _viewClampX.x, _viewClampX.y);
        yRotation = Mathf.Clamp(yRotation, _viewClampY.x, _viewClampY.y);

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
    }
}