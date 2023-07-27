using Player.AudioManagers;
using UnityEngine;

namespace Player
{
    public class FPSController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float mouseSensitivity = 2.0f;
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private CapsuleCollider standingCollider;
        [SerializeField] private CapsuleCollider crouchCollider;
        [SerializeField] private float sprintSpeed;
        [SerializeField] private float crouchSpeed;

        [SerializeField] private PlayerFootStepsAudioManager playerFootStepsAudioManager;

        private const float _crouchingOffset = .468f;
        private Rigidbody _rigidbody;
        private Vector3 _moveAmount;
        private float _verticalLookRotation;
        private Vector3 _smoothMoveVelocity;
        private float _currentMoveSpeed;
        private static bool _isCrouching;
        private float _walkFootstepTimer = .75f;
        private float _sprintFootstepTimer = .45f;
        private bool _isWalking;
        private bool _isSprinting;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            HandleSprinting();
            HandleCrouching();
            HandleMovement();
            if (_isWalking && !_isSprinting)
            {
                _walkFootstepTimer -= Time.deltaTime;
                if (_walkFootstepTimer <= 0)
                {
                    _walkFootstepTimer = .75f;
                    playerFootStepsAudioManager.PlayFootStepOneShot();
                }
            }

            if (_isSprinting)
            {
                _sprintFootstepTimer -= Time.deltaTime;
                if (_sprintFootstepTimer <= 0)
                {
                    _sprintFootstepTimer = .45f;
                    playerFootStepsAudioManager.PlayFootStepOneShot();
                }
            }
        }

        private void HandleMovement()
        {
            var mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            var mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            _verticalLookRotation -= mouseY;
            _verticalLookRotation = Mathf.Clamp(_verticalLookRotation, -90f, 90f);

            cameraTransform.localRotation = Quaternion.Euler(_verticalLookRotation, 0f, 0f);
            transform.Rotate(Vector3.up * mouseX);

            var moveX = Input.GetAxis("Horizontal");
            var moveZ = Input.GetAxis("Vertical");
            _moveAmount = (cameraTransform.forward * moveZ + cameraTransform.right * moveX).normalized * _currentMoveSpeed;
            _isWalking = _moveAmount != Vector3.zero;
            _moveAmount.y = 0f;
        }

        private void HandleSprinting()
        {
            if (_isCrouching) return;
            
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _currentMoveSpeed = sprintSpeed;
                _isSprinting = true;
                _isWalking = false;
            }
            else
            {
                _currentMoveSpeed = moveSpeed;
                _isSprinting = false;
            }
        }

        private void HandleCrouching()
        {
            if (Input.GetKeyDown(KeyCode.C) && !_isCrouching)
            {
                ChangeToCrouch(true);
            }
            else if (Input.GetKeyDown(KeyCode.C) && _isCrouching)
            {
                ChangeToCrouch(false);
            }
        }

        private void ChangeToCrouch(bool isCrouching)
        {
            Debug.Log(isCrouching);
            var cameraPosition = cameraTransform.position;
            if (isCrouching)
            {
                _isCrouching = true;
                crouchCollider.enabled = true;
                standingCollider.enabled = false;
                cameraTransform.position = new Vector3(cameraPosition.x, cameraPosition.y - _crouchingOffset, cameraPosition.z);
                _currentMoveSpeed = crouchSpeed;
            }
            else
            {
                _isCrouching = false;
                standingCollider.enabled = true;
                crouchCollider.enabled = false;
                cameraTransform.position = new Vector3(cameraPosition.x, cameraPosition.y + _crouchingOffset, cameraPosition.z);
                _currentMoveSpeed = moveSpeed;
            }
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = _moveAmount;
        }
    }
}