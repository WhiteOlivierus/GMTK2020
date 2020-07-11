using UnityEngine;

[CreateAssetMenu(fileName = "MouseState.asset", menuName = "GMTK2020/MouseState", order = 0)]
public class MouseEmotionState : ScriptableObject
{
    public MouseEmotion emotion = MouseEmotion.Calm;
    public CursorAnimation cursorAnimation = default;
    public DialogueObject dialogue = default;
}
