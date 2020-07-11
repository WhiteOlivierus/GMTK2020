using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string line = string.Empty;
    public AudioClip audio = default;
    public float delay = 0f;
}
