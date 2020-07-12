using DutchSkull.Singleton;
using System;
using System.Collections;
using UnityEngine;

public class SceneNavigation : Singleton<SceneNavigation>
{
    [SerializeField] private int fadeTime = 300;

    private Fader fader = default;
    private bool navigating = false;

    public Action playerMoved;

    private void Awake() => fader = gameObject.GetComponent<Fader>();

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
        fader.FadeOut();

        while (fader.Fading)
            yield return null;

        //Set player position to root position of navigation trigger
        PlayerData.transform.position = parent.position;
        PlayerData.transform.rotation = parent.rotation;

        if (playerMoved != null)
            playerMoved.Invoke();

        //Fade in
        fader.FadeIn();

        while (fader.Fading)
            yield return null;

        //Enable movement
        PlayerData.cameraController.active = true;
        navigating = false;
    }

    private PlayerData PlayerData => PlayerData.Instance;
}
