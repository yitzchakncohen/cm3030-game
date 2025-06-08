using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerActions;

public class InputManager : MonoBehaviour, ICharacterControllerActions
{
    public event Action<Vector2> OnMoveInput;
    public event Action<Vector2> OnLookInput;
    public event Action OnJumpInput;
    public event Action OnCrouchButtonDown;
    public event Action OnCrouchButtonUp;
    public event Action OnGrabInputDown;
    public event Action OnGrabInputUp;
    public event Action OnScanInputDown;
    public event Action OnScanInputUp;

    private PlayerActions playerActions;

    private void OnEnable()
    {
        if (playerActions == null)
        {
            playerActions = new PlayerActions();
            playerActions.CharacterController.SetCallbacks(this);
        }
        playerActions.Enable();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDisable()
    {
        playerActions.Disable();
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 moveVector = context.ReadValue<Vector2>();
        OnMoveInput?.Invoke(moveVector);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Vector2 look = context.ReadValue<Vector2>();
        OnLookInput?.Invoke(look);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnJumpInput?.Invoke();
        }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnCrouchButtonDown?.Invoke();
        }
        else if (context.canceled)
        {
            OnCrouchButtonUp?.Invoke();
        }
    }

    public void OnGrab(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnGrabInputDown?.Invoke();
        }
        else if (context.canceled)
        {
            OnGrabInputUp?.Invoke();
        }
    }

    public void OnScan(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnScanInputDown?.Invoke();
        }
        else if (context.canceled)
        {
            OnScanInputUp?.Invoke();
        }
    }
}
