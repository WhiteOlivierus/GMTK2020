using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "DialogueObject", menuName = "GMTK2020/DialogueObject", order = 0)]
public class DialogueObject : ScriptableObject
{
    public DialogueLine[] lines;
}
