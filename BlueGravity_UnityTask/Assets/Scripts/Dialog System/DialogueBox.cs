using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogueBox : MonoBehaviour
{
    public Action OnDialogueBoxOpenComplete;

    [SerializeField] private Text nameText;
    [SerializeField] private Text dialogueText;
    [SerializeField] private string openDialogueBoxAnimation;
    [SerializeField] private string closeDialogueBoxAnimation;
    [SerializeField] private Animator animator;
    private string sentence;

    public void SetName(string name)
    {
        nameText.text = name;
    }

    public void SetSentence(string nextSentence)
    {
        sentence = nextSentence;
    }

    public void OpenDialogueBox()
    {
        animator.SetBool("IsOpen", true);
    }

    public void DisplaySentence()
    {
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    private IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForEndOfFrame();
        }
    }

    public void CloseDialogueBox()
    {
        animator.SetBool("IsOpen", false);
    }

    public void OnOpenAnimationComplete()
    {
        OnDialogueBoxOpenComplete?.Invoke();
    }
}
