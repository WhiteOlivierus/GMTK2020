using System.Collections;
using UnityEngine;

public class Fader : MonoBehaviour
{
    [SerializeField] private int fadeTime = 300;
    [SerializeField] private CanvasGroup fadeGroup = default;

    public bool Fading { get; private set; } = false;

    public void Construct(int fadeTime, CanvasGroup fadeGroup)
    {
        this.fadeTime = fadeTime;
        this.fadeGroup = fadeGroup;
    }

    public void FadeIn() => StartCoroutine(FadingIn(fadeGroup));
    public void FadeOut() => StartCoroutine(FadingOut(fadeGroup));

    private IEnumerator FadingIn(CanvasGroup fadeGroup)
    {
        Fading = true;

        for (int i = fadeTime; i > 0; i--)
        {
            fadeGroup.alpha = (1f / fadeTime) * i;
            yield return null;
        }

        Fading = false;
    }

    private IEnumerator FadingOut(CanvasGroup fadeGroup)
    {
        Fading = true;

        for (int i = 0; i < fadeTime; i++)
        {
            fadeGroup.alpha = (1f / fadeTime) * i;
            yield return null;
        }

        Fading = false;
    }
}
