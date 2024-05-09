using UnityEngine;

public class SpeakingNPC : MonoBehaviour, IInteractable
{
    public Dialogue dialogue;

    public void RespondToInteraction()
    {
        TriggerDialogue();
    }

    public void EndInteraction()
    {
        EndDialogue();
    }

    protected virtual void TriggerDialogue()
    {
        DialogueManager.Instance.SetupDialogue(dialogue);
    }

    protected virtual void EndDialogue()
    {
        DialogueManager.Instance.EndDialogue();
    }
}
