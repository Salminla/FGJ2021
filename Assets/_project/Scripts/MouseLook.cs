using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [Range(20, 300)] 
    [SerializeField] private float mouseSensitivity = 150;
    [SerializeField] private bool invert = false;
    
    private float rotX;
    private float rotY;

    void Start()
    {
        CameraInit();
    }
    
    void Update()
    {
        Rotate();
    }

    private void CameraInit()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotX = rot.x;
        rotY = rot.y;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Rotate()
    {
        var mouseY = Input.GetAxis("Mouse X");
        var mouseX = Input.GetAxis("Mouse Y");

        rotX += !invert ? mouseX * -1 * mouseSensitivity * Time.deltaTime : mouseX * mouseSensitivity * Time.deltaTime;

        rotY += mouseY * mouseSensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -80, 45);
        
        Quaternion localRotationY = Quaternion.Euler(0, rotY, 0);
        Quaternion localRotationX = Quaternion.Euler(rotX, 0, 0);

        transform.rotation = localRotationY;
        mainCamera.transform.localRotation = localRotationX;
    }
}
