using DutchSkull.Singleton;
using System;
using System.Collections;
using UnityEngine;

public class SceneNavigation : Singleton<SceneNavigation>
{
    private Fader fader = default;
    private bool navigating = false;

    [HideInInspector] public NavigationRoot currentNavigationPoint = default;
    [HideInInspector] public NavigationRoot lastNavigationPoint = default;

    public Action playerMoved;

    private void Start()
    {
        fader = gameObject.GetComponent<Fader>();

        GameObject gameObject1 = GameObject.FindGameObjectWithTag("FirstNavPoint");

        PlayerData.transform.position = gameObject1.transform.parent.GetChild(0).position;
        PlayerData.transform.rotation = gameObject1.transform.parent.GetChild(0).rotation;

        if (playerMoved != null)
            playerMoved.Invoke();

        NavigationRoot navigationPointData = gameObject1.transform.parent.GetComponentInChildren<NavigationRoot>();

        currentNavigationPoint = navigationPointData;

        if (currentNavigationPoint.onArrival != null)
            currentNavigationPoint.onArrival.Invoke();
    }

    public void Navigate(GameObject navigationTrigger)
    {
        if (navigating)
            return;

        navigating = true;

        Debug.Log($"{nameof(SceneNavigation)}: Navigating too {navigationTrigger.name}");

        Transform parent = navigationTrigger.transform.parent.GetChild(0);

        if (!parent.TryGetComponent(out NavigationRoot navigationPointData))
            return;

        StartCoroutine(Move(parent, navigationPointData));
    }

    private IEnumerator Move(Transform parent, NavigationRoot navigationPointData)
    {
        //Disable movement
        PlayerData.cameraController.active = false;

        MouseController.showMouse = false;

        //Fade out
        fader.FadeOut();

        while (fader.Fading)
            yield return null;

        if (currentNavigationPoint != null)
            if (currentNavigationPoint.onExit != null)
                currentNavigationPoint.onExit.Invoke();

        //Set player position to root position of navigation trigger
        PlayerData.transform.position = parent.position;
        PlayerData.transform.rotation = parent.rotation;

        if (playerMoved != null)
            playerMoved.Invoke();

        //Fade in
        fader.FadeIn();

        while (fader.Fading)
            yield return null;

        navigating = false;

        lastNavigationPoint = currentNavigationPoint;
        currentNavigationPoint = navigationPointData;

        if (currentNavigationPoint.onArrival != null)
            currentNavigationPoint.onArrival.Invoke();

        //Enable movement
        PlayerData.cameraController.active = true;

        MouseController.showMouse = true;
    }

    private PlayerData PlayerData => PlayerData.Instance;
    private MouseController MouseController => MouseController.Instance;
}
