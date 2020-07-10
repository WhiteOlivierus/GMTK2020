using DutchSkull.Singleton;
using UnityEngine;

public class InputManager : SingleSceneSingleton<InputManager>
{
    protected override void Awake() => SetInstance(this);

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) ||
        Input.GetKeyDown(KeyCode.LeftArrow))
            PlayerData.cameraController.SetDirection(-1);

        if (Input.GetKeyDown(KeyCode.D) ||
            Input.GetKeyDown(KeyCode.RightArrow))
            PlayerData.cameraController.SetDirection(1);

        PlayerData.cameraController.Update();

        if (Input.GetMouseButtonDown(0))
        {
            GameObject navigationTrigger = GetRaycasted();

            if (navigationTrigger == null)
                return;

            SceneNavigation.Navigate(PlayerData, navigationTrigger);
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
