using DutchSkull.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MouseController : SingleSceneSingleton<MouseController>
{
    [SerializeField] private CursorMode cursorMode = CursorMode.Auto;
    [SerializeField] private Vector2 hotSpot = Vector2.zero;

    private List<MouseEmotionState> emotions = new List<MouseEmotionState>();

    private MouseEmotionState currentState = default;

    protected override void Awake() => SetInstance(this);

    private void Start()
    {
        LoadStates();
        SetMouseState(MouseEmotion.Calm);
    }

    private void LoadStates() => emotions = Resources.LoadAll<MouseEmotionState>("States/").ToList();

    private void Update() => Cursor.SetCursor(currentState.cursorAnimation[0], hotSpot, cursorMode);

    public void SetMouseState(MouseEmotion emotion) => currentState = emotions.Where(x => x.emotion == emotion).First();
}


public enum MouseEmotion
{
    Calm,
    Grumpy,
    Murdery
}
