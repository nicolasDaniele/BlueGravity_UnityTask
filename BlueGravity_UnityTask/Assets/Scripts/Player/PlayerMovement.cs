using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    
    private Rigidbody2D body2D;
    private Vector2 movementDirection;
 
    private void Awake() 
    {
        body2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() 
    {
        PlayerInputComponent.OnMovementPerformed += UpdateDirection;
    }

    private void OnDisble() 
    {
        PlayerInputComponent.OnMovementPerformed -= UpdateDirection;
    }

    private void UpdateDirection(Vector2 newDirection)
    {
        movementDirection = newDirection;
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
