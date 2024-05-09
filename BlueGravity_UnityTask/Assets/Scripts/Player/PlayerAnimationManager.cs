using System;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    [SerializeField] private string speedParamName = "MovementSpeed";

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake() 
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable() 
    {
        PlayerInputComponent.OnMovementPerformed += SetMovementAnimation;
    }

    private void OnDisable() 
    {
        PlayerInputComponent.OnMovementPerformed -= SetMovementAnimation;
    }

    private void SetMovementAnimation(Vector2 inputVector)
    {
        animator.SetFloat(speedParamName, inputVector.magnitude);
        
        if(MathF.Abs(inputVector.x) > 0)
        {
            spriteRenderer.flipX  = inputVector.x < 0;
        }
    }
}
