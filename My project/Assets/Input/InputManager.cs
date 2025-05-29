using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerActions;

public class InputManager : MonoBehaviour, ICharacterControllerActions
{
    public event Action<Vector2> OnMoveInput;
    public event Action<float> OnLookHorizontalInput;
    public event Action<float> OnLookVerticalInput;
    public event Action OnJumpInput;
    public event Action OnCrouchInput;

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
        OnLookHorizontalInput?.Invoke(look.x);
        OnLookVerticalInput?.Invoke(look.y);
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
        if(context.started)
        {
            OnCrouchInput?.Invoke();
        }
    }
}
