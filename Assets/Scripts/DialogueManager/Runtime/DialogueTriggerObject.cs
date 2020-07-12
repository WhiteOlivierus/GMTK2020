using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTriggerObject : MonoBehaviour
{
    public bool triggerByTouch;

    [SerializeField] private DialogueObject dialogue = default;
    [SerializeField] private UnityEvent preDialogueMethod = default;
    [SerializeField] private UnityEvent dialogueMethod = default;
    public bool Interacted { get; set; }

    public void Interact()
    {
        Interacted = true;
        dialogueMethod.AddListener(() => { Interacted = false; Done(); });

        TriggerDialogue();

        if (preDialogueMethod != null)
            preDialogueMethod.Invoke();

        Done();
    }

    private void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Interactable");
        GetComponent<Collider>().isTrigger = triggerByTouch;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!triggerByTouch ||
            !other.CompareTag("Player"))
            return;

        Interact();
    }

    private void TriggerDialogue()
    {
        if (dialogue == null)
        {
            Debug.LogError($"{nameof(DialogueTriggerObject)}: {gameObject.name} has no dialogue object.");
            return;
        }

        if (dialogue.lines.Length < 1)
        {
            Debug.LogError($"{nameof(DialogueTriggerObject)}: {gameObject.name} has no dialogue lines.");
            return;
        }

        TMP_Text tMP_Text = gameObject.GetComponentInChildren<TMP_Text>();

        if (tMP_Text)
            DialogueManager.Instance.StartDialogue(dialogue);
        else
            DialogueManager.Instance.StartDialogue(dialogue);
    }

    [ExecuteInEditMode]
    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "DialogueGizmo.png", true);

        if (!triggerByTouch)
            return;

        Gizmos.color = new Color(0, 0, 0.5f, 0.25f);
        Gizmos.DrawCube(transform.position, transform.localScale);
    }

    public void Done() => enabled = false;
}
