using DutchSkull.Singleton;
using System;
using UnityEngine;

public class InputManager : SingleSceneSingleton<InputManager>
{
    protected override void Awake() => SetInstance(this);

    private void Update()
    {
        PlayerData.cameraController.Update();

        if (Input.GetKeyDown(KeyCode.A) ||
        Input.GetKeyDown(KeyCode.LeftArrow))
            PlayerData.cameraController.SetDirection(-1);

        if (Input.GetKeyDown(KeyCode.D) ||
            Input.GetKeyDown(KeyCode.RightArrow))
            PlayerData.cameraController.SetDirection(1);

        GameObject navigationTrigger = GetRaycasted();

        if (navigationTrigger != null &&
            navigationTrigger.TryGetComponent(out NavigationPoint point))
            point.OnHover();

        if (Input.GetMouseButtonDown(0))
        {
            SceneNavigation.Navigate(PlayerData, navigationTrigger, out NavigationPointRoot navigationPointData);

            if (navigationPointData == null)
                return;

            PlayerData.currentNavigationPoint = navigationPointData;
        }
    }

    private GameObject GetRaycasted()
    {
        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out hit))
            return null;

        return hit.transform.gameObject;
    }

    private SceneNavigation SceneNavigation => SceneNavigation.Instance;

    private PlayerData PlayerData => PlayerData.Instance;
}
