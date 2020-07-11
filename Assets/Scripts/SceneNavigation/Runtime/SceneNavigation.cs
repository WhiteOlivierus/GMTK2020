using DutchSkull.Singleton;
using System.Collections;
using UnityEngine;

public class SceneNavigation : Singleton<SceneNavigation>
{
    private const int FADE_TIME = 300;
    private bool fading = false;

    public void Navigate(PlayerData playerData, GameObject navigationTrigger, out NavigationPointData navigationPointData)
    {
        Debug.Log(navigationTrigger.name);

        Transform parent = navigationTrigger.transform.parent;

        if (!parent.TryGetComponent(out navigationPointData))
            return;

        StartCoroutine(Move(playerData, parent));
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
