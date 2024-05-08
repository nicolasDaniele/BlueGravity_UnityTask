using UnityEngine;
using System;

public class TestInteractableNPC : MonoBehaviour, IInteractable
{
    public static Action OnShopkeeperInteract;

    public void RespondToInteraction()
    {
        OnShopkeeperInteract?.Invoke();
    }
}
