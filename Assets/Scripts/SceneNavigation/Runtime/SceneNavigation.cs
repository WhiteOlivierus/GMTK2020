using DutchSkull.Singleton;
using System.Collections;
using UnityEngine;

public class SceneNavigation : Singleton<SceneNavigation>
{
    private const int FADE_TIME = 300;
    private const string TAG = "NavigationPoint";
    private bool fading = false;

    public void Navigate(PlayerData playerData, GameObject navigationTrigger)
    {
        Debug.Log(navigationTrigger.name);

        if (!navigationTrigger.transform.parent.CompareTag(TAG))
            return;

        StartCoroutine(Move(playerData, navigationTrigger.transform.parent));
    }

    private IEnumerator Move(PlayerData playerData, Transform parent)
    {
        //Disable movement
        playerData.cameraController.active = false;

        //Fade out
        StartCoroutine(FadeOut(playerData.fadeGroup));

        while (fading)
            yield return null;

        //Set player position to root position of navigation trigger
        playerData.transform.position = parent.position;
        playerData.transform.rotation = parent.rotation;

        //Fade in
        StartCoroutine(FadeIn(playerData.fadeGroup));

        while (fading)
            yield return null;

        //Enable movement
        playerData.cameraController.active = true;
    }

    public IEnumerator FadeIn(CanvasGroup fadeGroup)
    {
        fading = true;

        for (int i = FADE_TIME; i > 0; i--)
        {
            fadeGroup.alpha = (1f / FADE_TIME) * i;
            yield return null;
        }

        fading = false;
    }

    public IEnumerator FadeOut(CanvasGroup fadeGroup)
    {
        fading = true;

        for (int i = 0; i < FADE_TIME; i++)
        {
            fadeGroup.alpha = (1f / FADE_TIME) * i;
            yield return null;
        }

        fading = false;
    }
}
