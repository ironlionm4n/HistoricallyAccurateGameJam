using UnityEngine;

public class FPSController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float mouseSensitivity = 2.0f;
    [SerializeField] private Transform cameraTransform;
    
    private Rigidbody _rigidbody;
    private Vector3 _moveAmount;
    private float _verticalLookRotation;
    private Vector3 _smoothMoveVelocity;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        var mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        var mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        _verticalLookRotation -= mouseY;
        _verticalLookRotation = Mathf.Clamp(_verticalLookRotation, -90f, 90f);
        
        cameraTransform.localRotation = Quaternion.Euler(_verticalLookRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        var moveX = Input.GetAxis("Horizontal");
        var moveZ = Input.GetAxis("Vertical");
        _moveAmount = (cameraTransform.forward * moveZ + cameraTransform.right * moveX).normalized * moveSpeed;
        _moveAmount.y = 0f;

    }

    private void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + _moveAmount * Time.fixedDeltaTime);
    }
}
