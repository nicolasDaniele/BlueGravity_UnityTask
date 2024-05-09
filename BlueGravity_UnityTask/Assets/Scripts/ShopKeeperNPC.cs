using System;

public class ShopKeeperNPC : SpeakingNPC
{
    public static Action OnShopkeeperInteract;
    public static Action OnShopkeeperEndedInteraction;

    protected override void TriggerDialogue()
    {
        base.TriggerDialogue();
        OnShopkeeperInteract?.Invoke();
    }

    protected override void EndDialogue()
    {
        base.EndDialogue();
        OnShopkeeperEndedInteraction?.Invoke();
    }
}
