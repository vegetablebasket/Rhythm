using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Header("References")]
    public Transform cameraTransform;

    [Header("Look Settings")]
    public float mouseSensitivity = 450f;
    public float minPitch = -80f;
    public float maxPitch = 80f;

    [Header("Control")]
    public bool canLook = false;

    private float pitch = 0f;

    private void Start()
    {
        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }

        // 开始界面默认不锁鼠标
        SetLookEnabled(false);
    }

    private void Update()
    {
        if (!canLook || cameraTransform == null)
        {
            return;
        }

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        cameraTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }

    public void SetLookEnabled(bool enabled)
    {
        canLook = enabled;

        if (enabled)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}