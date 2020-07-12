using DutchSkull.Singleton;
using System;
using System.Collections;
using UnityEngine;

public class SceneNavigation : Singleton<SceneNavigation>
{
    [SerializeField] private int fadeTime = 300;

    private bool fading = false;
    private bool navigating = false;

    public Action playerMoved;

    private void Start()
    {
        //Disable movement
        PlayerData.cameraController.active = false;

        //Fade in
        StartCoroutine(FadeIn(PlayerData.fadeGroup));

        //while (fading)
        //    yield return null;

        //Enable movement
        PlayerData.cameraController.active = true;
        navigating = false;
    }

    public void Navigate(PlayerData playerData, GameObject navigationTrigger, out NavigationRoot navigationPointData)
    {
        navigationPointData = default;

        if (navigating)
            return;

        navigating = true;

        Debug.Log($"{nameof(SceneNavigation)}: Navigating too {navigationTrigger.name}");

        Transform parent = navigationTrigger.transform.parent.GetChild(0);

        if (!parent.TryGetComponent(out navigationPointData))
            return;

        StartCoroutine(Move(parent));
    }

    private IEnumerator Move(Transform parent)
    {
        //Disable movement
        PlayerData.cameraController.active = false;

        //Fade out
        StartCoroutine(FadeOut(PlayerData.fadeGroup));

        while (fading)
            yield return null;

        //Set player position to root position of navigation trigger
        PlayerData.transform.position = parent.position;
        PlayerData.transform.rotation = parent.rotation;

        if (playerMoved != null)
            playerMoved.Invoke();

        //Fade in
        StartCoroutine(FadeIn(PlayerData.fadeGroup));

        while (fading)
            yield return null;

        //Enable movement
        PlayerData.cameraController.active = true;
        navigating = false;
    }

    public IEnumerator FadeIn(CanvasGroup fadeGroup)
    {
        fading = true;

        for (int i = fadeTime; i > 0; i--)
        {
            fadeGroup.alpha = (1f / fadeTime) * i;
            yield return null;
        }

        fading = false;
    }

    public IEnumerator FadeOut(CanvasGroup fadeGroup)
    {
        fading = true;

        for (int i = 0; i < fadeTime; i++)
        {
            fadeGroup.alpha = (1f / fadeTime) * i;
            yield return null;
        }

        fading = false;
    }

    private PlayerData PlayerData => PlayerData.Instance;
}
