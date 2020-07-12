using UnityEngine;
using TMPro;
using System.Collections;
using DutchSkull.Singleton;

public class DialogueManager : Singleton<DialogueManager>
{
    [SerializeField] private TMP_Text textObject = default;
    [SerializeField] private GameObject panel = default;

    private DialogueObject currentDialogue = default;

    private int dialogueIndex = default;

    private IEnumerator TriggerNextDialogue(float time)
    {
        yield return new WaitForSeconds(time);

        NextDialogue();
    }

    private void NextDialogue()
    {
        dialogueIndex++;

        if (dialogueIndex < currentDialogue.lines.Length)
        {
            Debug.Log("Show dialogue");
            ShowDialogue(currentDialogue.lines[dialogueIndex]);
            return;
        }
        else
        {
            currentDialogue = null;
            dialogueIndex = 0;
            textObject.text = string.Empty;
            panel.SetActive(false);

            return;
        }
    }

    public void StartDialogue(DialogueObject dialogue)
    {
        if (!CheckDialogue(dialogue))
            return;

        panel.SetActive(true);

        ShowDialogue(dialogue.lines[dialogueIndex]);
    }

    private void ShowDialogue(DialogueLine dialogueLine)
    {
        StopAllCoroutines();

        if (dialogueLine.audio != null)
            StartCoroutine(TypeWriteText(textObject, dialogueLine.line, dialogueLine.audio.length));
        else
            StartCoroutine(TypeWriteText(textObject, dialogueLine.line));

        if (dialogueLine.audio != null)
            AudioManager.Instance.PlayDialogueAudio(dialogueLine.audio);

        if (dialogueLine.audio != null)
            StartCoroutine(TriggerNextDialogue(dialogueLine.audio.length + dialogueLine.delay));
        else
            StartCoroutine(TriggerNextDialogue(2f + dialogueLine.delay));
    }

    IEnumerator TypeWriteText(TMP_Text container, string text, float duration = 1f)
    {
        int AmountOfCharactersPossible = 0;

        container.text = "";

        float step = Time.deltaTime * text.Length / duration;
        for (float i = 0; i < text.Length; i += step)
        {
            int charactersTyped = (int)Mathf.Clamp(i, 0f, text.Length - 1);
            int beginIndex = AmountOfCharactersPossible == 0 ? AmountOfCharactersPossible : charactersTyped - AmountOfCharactersPossible;

            container.text = text.Substring(beginIndex, charactersTyped - beginIndex);

            container.ForceMeshUpdate();

            if (container.isTextTruncated && AmountOfCharactersPossible == 0)
                AmountOfCharactersPossible = charactersTyped;

            yield return null;
        }

        container.text = text;
    }

    private bool CheckDialogue(DialogueObject dialogue)
    {
        if (currentDialogue != null &&
            currentDialogue == dialogue ||
            dialogue == null ||
            dialogue.lines == null ||
            dialogue.lines.Length == 0)
            return false;

        currentDialogue = dialogue;
        dialogueIndex = 0;

        return true;
    }
}
