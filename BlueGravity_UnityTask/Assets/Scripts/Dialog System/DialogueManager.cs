using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : SingletonMonoBehaviour<DialogueManager>
{
    [SerializeField] private DialogueBox dialogueBox;
    private Queue<string> sentences = new Queue<string>();

    private void Start() 
    {
        dialogueBox.gameObject.SetActive(false);    
    }

    public void SetupDialogue(Dialogue dialogue)
    {
        dialogueBox.gameObject.SetActive(true);
        dialogueBox.SetName(dialogue.name);
        dialogueBox.OpenDialogueBox();
        dialogueBox.OnDialogueBoxOpenComplete += DisplayNextSentence;
        
        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            return;
        }

        string sentence = sentences.Dequeue();

        dialogueBox.SetSentence(sentence);
        dialogueBox.DisplaySentence();
    }

    public void EndDialogue()
    {
        dialogueBox.CloseDialogueBox();
    }
}
