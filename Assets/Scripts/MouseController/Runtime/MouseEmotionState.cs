using UnityEngine;

[CreateAssetMenu(fileName = "MouseState.asset", menuName = "GMTK2020/MouseState", order = 0)]
public class MouseEmotionState : ScriptableObject
{
    public MouseEmotion emotion = MouseEmotion.Calm;
    public Texture2D[] cursorAnimation = default;
    public AudioClip[] speechLines = default;
}
