using System;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private InputManager inputManager;
    private Rigidbody rigidBody;
    [SerializeField] private Transform cameraHolder;
    [SerializeField] private float speed;
    [SerializeField] private float cameraHorizontalSensitivity = 10f;
    [SerializeField] private float cameraVerticalSensitivity = 10f;
    [SerializeField] private Vector2 verticalLookClamp;
    private Vector3 moveVelocity;
    private Vector2 lookVector;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        rigidBody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        inputManager.OnMoveInput += InputManager_OnMoveInput;
        inputManager.OnLookInput += InputManager_OnLookInput;
        inputManager.OnCrouchInput += InputManager_OnCrouchInput;
        inputManager.OnJumpInput += InputManager_OnJumpInput;
    }

    private void OnDisable()
    {
        inputManager.OnMoveInput -= InputManager_OnMoveInput;
        inputManager.OnLookInput -= InputManager_OnLookInput;
        inputManager.OnCrouchInput -= InputManager_OnCrouchInput;
        inputManager.OnJumpInput -= InputManager_OnJumpInput;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        Look();
    }

    /* --- Code modified from my previous implementation --- */
    /* --- https://community.gamedev.tv/t/unity-fps-camera-rotation/229608/3 --- */
    private void Look()
    {
        //Horizontal Rotation in quaternions
        float horizontalRotationDelta = lookVector.x * cameraHorizontalSensitivity * Time.deltaTime;
        cameraHolder.localRotation *= Quaternion.AngleAxis(horizontalRotationDelta, Vector3.up);

        //Vertical Rotation in quaternions
        float verticalRotationDelta = -lookVector.y * cameraVerticalSensitivity * Time.deltaTime;
        cameraHolder.localRotation *= Quaternion.AngleAxis(verticalRotationDelta, Vector3.right);

        //Clamp vertical rotation
        var eulerAngles = cameraHolder.localEulerAngles;
        eulerAngles.z = 0;

        var verticalAngle = cameraHolder.localEulerAngles.x;

        if (verticalAngle > 180 && verticalAngle < 360 - verticalLookClamp.y)
        {
            eulerAngles.x = 360 - verticalLookClamp.y;
        }
        else if (verticalAngle < 180 && verticalAngle > -verticalLookClamp.x)
        {
            eulerAngles.x = -verticalLookClamp.x;
        }

        cameraHolder.localEulerAngles = eulerAngles;
    }

    private void Move()
    {
        rigidBody.linearVelocity = moveVelocity;
    }

    private void InputManager_OnMoveInput(Vector2 moveInput)
    {
        Vector3 moveVector = moveInput.x * cameraHolder.right + moveInput.y * cameraHolder.forward;
        moveVelocity = moveVector.normalized * speed;
    }

    private void InputManager_OnLookInput(Vector2 lookVector)
    {
        this.lookVector = lookVector;
    }

    private void InputManager_OnCrouchInput()
    {
    }

    private void InputManager_OnJumpInput()
    {
    }
}
