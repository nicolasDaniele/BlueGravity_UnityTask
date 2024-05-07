using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    
    private Rigidbody2D body2D;
    private Vector2 movementDirection;
    private PlayerInput playerInput;
 
    private void Awake() 
    {
        body2D = GetComponent<Rigidbody2D>();

        playerInput = GetComponent<PlayerInput>();
        PlayerInputActions playerInputActions = new PlayerInputActions();
        playerInputActions.PlayerActionMap.Enable();
        playerInputActions.PlayerActionMap.Movement.performed += UpdateDirection; 
        playerInputActions.PlayerActionMap.Movement.canceled += UpdateDirection;  
    }

    private void UpdateDirection(InputAction.CallbackContext context)
    {
        movementDirection = context.ReadValue<Vector2>();
    }

    private void FixedUpdate() 
    {
        Move(movementDirection);
    }

    private void Move(Vector2 direction)
    {
        if(body2D != null)
        {
            body2D.MovePosition(body2D.position + direction * movementSpeed * Time.fixedDeltaTime);
        }       
    }
}
