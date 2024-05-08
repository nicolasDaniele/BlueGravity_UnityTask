using UnityEngine;

public class InteractionComponent : MonoBehaviour
{
    private bool canInteract = false;
    private IInteractable interactable = null;

    private void OnEnable() 
    {
        PlayerInputComponent.OnInteractionPerformed += Interact;
    }

    private void OnDisble() 
    {
        PlayerInputComponent.OnInteractionPerformed -= Interact;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        interactable = other.GetComponent<IInteractable>();
        if(interactable != null)
        {
            canInteract = true;
        }        
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.GetComponent<IInteractable>() != null)
        {
            canInteract = false;
            interactable = null;
        }
    }

    private void Interact()
    {
        if(interactable != null && canInteract)
        {
            interactable.RespondToInteraction();
        }
    }
}
