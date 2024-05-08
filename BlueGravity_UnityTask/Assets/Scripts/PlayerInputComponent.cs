using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerInputComponent : MonoBehaviour
{
    public static Action<Vector2> OnMovementPerformed;
    public static Action OnInteractionPerformed;

    private PlayerInput playerInput;

    private void Awake() 
    {
        PlayerInputActions playerInputActions = new PlayerInputActions();
        playerInputActions.PlayerActionMap.Enable();

        playerInputActions.PlayerActionMap.Movement.performed += Movement_Performed; 
        playerInputActions.PlayerActionMap.Movement.canceled += Movement_Performed;
        playerInputActions.PlayerActionMap.Interact.performed += Interaction_Performed;
    }

    private void Movement_Performed(InputAction.CallbackContext context)
    {
        OnMovementPerformed?.Invoke(context.ReadValue<Vector2>());
    }

    private void Interaction_Performed(InputAction.CallbackContext context)
    {
        OnInteractionPerformed?.Invoke();
    }
}
