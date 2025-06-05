using System;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private InputManager inputManager;
    private Rigidbody rigidBody;
    [SerializeField] private Transform head;
    [SerializeField] private Transform hand;
    [SerializeField] private Transform crouchPosition;
    [SerializeField] private Transform feet;
    [SerializeField] private float feetRadius = 0.1f;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float cameraHorizontalSensitivity = 10f;
    [SerializeField] private float cameraVerticalSensitivity = 10f;
    [SerializeField] private float pickupDistance = 2f;
    [SerializeField] private float grabForce = 10f;
    [SerializeField] private Vector2 verticalLookClamp;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask pickupsLayer;
    private Pickup grabbedPickup = null;
    private Pickup targetPickup = null;
    private Vector2 moveInput;
    private Vector2 lookVector;
    private Vector3 headUpPosition;
    private bool isGrounded = false;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        rigidBody = GetComponent<Rigidbody>();
        headUpPosition = head.localPosition;
    }

    private void OnEnable()
    {
        inputManager.OnMoveInput += InputManager_OnMoveInput;
        inputManager.OnLookInput += InputManager_OnLookInput;
        inputManager.OnCrouchButtonDown += InputManager_OnCrouchButtonDown;
        inputManager.OnCrouchButtonUp += InputManager_OnCrouchButtonUp;
        inputManager.OnJumpInput += InputManager_OnJumpInput;
        inputManager.OnGrabInputDown += InputManager_OnGrabInputDown;
        inputManager.OnGrabInputUp += InputManager_OnGrabInputUp;
    }

    private void OnDisable()
    {
        inputManager.OnMoveInput -= InputManager_OnMoveInput;
        inputManager.OnLookInput -= InputManager_OnLookInput;
        inputManager.OnCrouchButtonDown -= InputManager_OnCrouchButtonDown;
        inputManager.OnCrouchButtonUp -= InputManager_OnCrouchButtonUp;
        inputManager.OnJumpInput -= InputManager_OnJumpInput;
        inputManager.OnGrabInputDown -= InputManager_OnGrabInputDown;
        inputManager.OnGrabInputUp -= InputManager_OnGrabInputUp;
    }

    private void FixedUpdate()
    {
        Move();
        MoveGrabbedPickup();
    }

    private void Update()
    {
        Look();
        CheckIsGround();
        CheckForPickups();
    }

    /* --- Code modified from my previous implementation --- */
    /* --- https://community.gamedev.tv/t/unity-fps-camera-rotation/229608/3 --- */
    private void Look()
    {
        //Horizontal Rotation in quaternions
        float horizontalRotationDelta = lookVector.x * cameraHorizontalSensitivity * Time.deltaTime;
        head.localRotation *= Quaternion.AngleAxis(horizontalRotationDelta, Vector3.up);

        //Vertical Rotation in quaternions
        float verticalRotationDelta = -lookVector.y * cameraVerticalSensitivity * Time.deltaTime;
        head.localRotation *= Quaternion.AngleAxis(verticalRotationDelta, Vector3.right);

        //Clamp vertical rotation
        var eulerAngles = head.localEulerAngles;
        eulerAngles.z = 0;

        var verticalAngle = head.localEulerAngles.x;

        if (verticalAngle > 180 && verticalAngle < 360 - verticalLookClamp.y)
        {
            eulerAngles.x = 360 - verticalLookClamp.y;
        }
        else if (verticalAngle < 180 && verticalAngle > -verticalLookClamp.x)
        {
            eulerAngles.x = -verticalLookClamp.x;
        }

        head.localEulerAngles = eulerAngles;
    }

    private void Move()
    {
        Vector3 moveVector = moveInput.x * head.right + moveInput.y * head.forward;
        moveVector = Vector3.ProjectOnPlane(moveVector, Vector3.up);
        Vector3 moveVelocity = moveVector.normalized * speed;
        Vector3 moveForce = moveVelocity - new Vector3(rigidBody.linearVelocity.x, 0f, rigidBody.linearVelocity.z);
        rigidBody.AddForce(moveForce, ForceMode.VelocityChange);
    }

    private void MoveGrabbedPickup()
    {
        if (grabbedPickup != null)
        {
            grabbedPickup.MoveToTarget(hand, grabForce);
        }
    }

    private void CheckIsGround()
    {
        isGrounded = Physics.CheckSphere(feet.position, feetRadius, groundLayer);
    }

    private void CheckForPickups()
    {
        if (grabbedPickup != null) return;

        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * pickupDistance, Color.red);
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, pickupDistance, pickupsLayer))
        {
            if (hit.transform.TryGetComponent<Pickup>(out Pickup pickup))
            {
                if (pickup != targetPickup)
                {
                    targetPickup?.Reset();
                    targetPickup = pickup;
                    targetPickup.Target();
                }
            }
        }
        else
        {
            targetPickup?.Reset();
            targetPickup = null;
        }
    }

    private void InputManager_OnMoveInput(Vector2 moveInput)
    {
        this.moveInput = moveInput;
    }

    private void InputManager_OnLookInput(Vector2 lookVector)
    {
        this.lookVector = lookVector;
    }

    private void InputManager_OnCrouchButtonDown()
    {
        head.localPosition = crouchPosition.localPosition;
    }

    private void InputManager_OnCrouchButtonUp()
    {
        head.localPosition = headUpPosition;
    }

    private void InputManager_OnJumpInput()
    {
        if (isGrounded)
        {
            rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void InputManager_OnGrabInputDown()
    {
        if (targetPickup == null) return;
        grabbedPickup = targetPickup;
        grabbedPickup.Grab();
        grabbedPickup.transform.SetParent(hand);
        grabbedPickup.transform.position = hand.position;
    }
    
    private void InputManager_OnGrabInputUp()
    {
        if (grabbedPickup == null) return;
        grabbedPickup.transform.SetParent(null);
        grabbedPickup.Reset();
        grabbedPickup = null;
    }
}
