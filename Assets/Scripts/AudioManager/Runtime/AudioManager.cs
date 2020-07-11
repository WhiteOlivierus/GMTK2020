using DutchSkull.Singleton;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    private AudioSource dialogueSource = default;

    private void Awake()
    {
        dialogueSource = gameObject.AddComponent<AudioSource>();
        dialogueSource.loop = false;
        dialogueSource.playOnAwake = false;
    }

    public void PlayDialogueAudio(AudioClip audio) => PlayAudio(audio, dialogueSource);

    private void PlayAudio(AudioClip audio, AudioSource source) => source.PlayOneShot(audio);
}
