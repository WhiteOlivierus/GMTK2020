using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MultipleEvents : MonoBehaviour
{
    [SerializeField] private List<UnityEvent> dialogues = new List<UnityEvent>();

    private int currentDialogueIndex = 0;

    public void IncrementDialogue() => currentDialogueIndex++;

    public void PlayCurrentDialogue()
    {
        if (currentDialogueIndex > dialogues.Count)
            currentDialogueIndex = dialogues.Count - 1;


        if( dialogues[currentDialogueIndex] != null)
            dialogues[currentDialogueIndex].Invoke();
    }

    private DialogueManager DialogueManager => DialogueManager.Instance;
}
