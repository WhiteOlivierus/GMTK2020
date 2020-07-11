using DutchSkull.Singleton;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MouseController : SingleSceneSingleton<MouseController>
{
    [SerializeField] private CursorMode cursorMode = CursorMode.Auto;
    [SerializeField] private Vector2 hotSpot = Vector2.zero;

    private List<MouseEmotionState> emotions = new List<MouseEmotionState>();

    private MouseEmotionState currentState = default;

    private int animationIndex = 0;

    protected override void Awake() => SetInstance(this);

    private void Start()
    {
        LoadStates();
        SetMouseState(MouseEmotion.Calm);
    }

    private void LoadStates() => emotions = Resources.LoadAll<MouseEmotionState>("States/").ToList();

    private void Update() => LoopAnimation(currentState.cursorAnimation);

    private void LoopAnimation(CursorAnimation cursorAnimation)
    {
        if (animationIndex >= cursorAnimation.animation.Length)
            animationIndex = 0;

        Cursor.SetCursor(cursorAnimation.animation[animationIndex], hotSpot, cursorMode);

        animationIndex++;
    }

    public void SetMouseState(MouseEmotion emotion)
    {
        currentState = emotions.Where(states => states.emotion == emotion).First();
        DialogueManager.StartDialogue(currentState.dialogue);
    }

    public void SetMouseState(MouseEmotionState state)
    {
        currentState = state;
        DialogueManager.StartDialogue(currentState.dialogue);
    }

    private DialogueManager DialogueManager => DialogueManager.Instance;
}
