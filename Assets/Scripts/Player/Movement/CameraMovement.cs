using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float xSensitivity = 1f;
    [SerializeField] private float ySensitivity = 1f;

    [SerializeField] private Transform playerRBTransform;
    [SerializeField] private Transform playerObjectTransform;

    private float xRotation;
    private float yRotation;
    
    private void Awake()
    {
        Cursor.visible = !Cursor.visible;
        Cursor.lockState = CursorLockMode.Locked; 
    }

    private void Update()
    {
        LookAround();
    }

    private void LookAround()
    {
        Vector2 lookAmt = Mouse.current.delta.ReadValue();
        xRotation -= lookAmt.y * xSensitivity;
        yRotation += lookAmt.x * ySensitivity;
        Mathf.Clamp(xRotation, -45f, 45f);
        Mathf.Clamp(yRotation, -45f, 45f);

        
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        playerRBTransform.rotation = Quaternion.Euler(0f, yRotation, 0f);
        
    }
}
