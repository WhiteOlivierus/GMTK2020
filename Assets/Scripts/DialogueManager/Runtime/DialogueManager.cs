using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System.Collections;
using DutchSkull.Singleton;

public class DialogueManager : Singleton<DialogueManager>
{
    private GameObject dialogueBox;
    [SerializeField] private TMP_Text textObject;
    //private AudioSource dialogueAudio;

    private UnityEvent dialogueMethod;
    private DialogueObject currentDialogue;
    private int dialogueIndex;
    private bool dialogueMode = false;
    private bool firstLine;
    private bool custom = false;

    private void Update()
    {
        if (!dialogueMode)
            return;

        if (firstLine)
        {
            firstLine = false;
            return;
        }

        NextDialogue();
    }

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
            ShowDialogue(currentDialogue.lines[dialogueIndex]);
            return;
        }
        else
        {
            dialogueMode = false;

            //if (!custom)
            //    dialogueBox.SetActive(false);

            currentDialogue = null;
            dialogueIndex = 0;
            custom = false;

            StopAllCoroutines();

            if (dialogueMethod == null)
                return;

            dialogueMethod.Invoke();

            return;
        }
    }

    public void StartDialogue(DialogueObject dialogue, GameObject box = default, UnityEvent dm = null)
    {
        if (!CheckDialogue(dialogue))
            return;

        dialogueMode = true;
        dialogueMethod = dm;

        if (box != default)
        {
            //dialogueBox = box;
            custom = true;
        }
        else
        {
            //dialogueBox = null;
        }

        //GetDialogueBox();

        //InnitDialogueBox();

        if (dialogueIndex == 0)
        {
            firstLine = true;
            //dialogueBox.SetActive(true);
            ShowDialogue(dialogue.lines[dialogueIndex]);
        }
        else
        {
            dialogueMode = true;
        }
    }

    private void InnitDialogueBox()
    {
        //dialogueAudio = GameObject.Find("DialogueSource").GetComponentInChildren<AudioSource>();

        //if (dialogueAudio == null)
        //dialogueAudio = dialogueBox.AddComponent<AudioSource>();

        dialogueBox.SetActive(false);
    }

    private void GetDialogueBox()
    {
        //if (dialogueBox != null)
        return;

        //dialogueBox = GameObject.Find("DialogueBox");

        if (dialogueBox != null)
            return;

        //GameObject original = (GameObject)Resources.Load("Prefabs/DialogueBox");
        //dialogueBox = Instantiate(original, transform);
        //dialogueBox.name = original.name;
    }

    private void ShowDialogue(DialogueLine dialogueLine)
    {
        StopAllCoroutines();

        try
        {
            StartCoroutine(TypeWriteText(textObject, dialogueLine.line));
        }
        catch
        {
            Debug.LogWarning(dialogueBox.name + " is missing a speaker box");
        }

        if (dialogueLine.audio != null && dialogueLine.audio != default)
            StartCoroutine(TriggerNextDialogue(dialogueLine.audio.length));
        else
            StartCoroutine(TriggerNextDialogue(2f));

        AudioManager.Instance.PlayDialogueAudio(dialogueLine.audio);
    }

    IEnumerator TypeWriteText(TMP_Text container, string text, float duration = 1f)
    {
        int AmountOfCharactersPossible = 0;

        container.text = "";

        for (float t = 0; t < text.Length; t += Time.deltaTime * text.Length / duration)
        {
            int charactersTyped = (int)Mathf.Clamp(t, 0f, text.Length - 1);
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
