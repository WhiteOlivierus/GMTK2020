using DutchSkull.Singleton;
using UnityEngine;

public class InputManager : SingleSceneSingleton<InputManager>
{
    private int direction = 0;
    private bool directionChanged = false;

    protected override void Awake() => SetInstance(this);

    [SerializeField] private MouseEmotionState testState = default;

    private void Update()
    {
        TurnCamera();

        Interact();

        ChangeMouseState();

        if (directionChanged)
        {
            directionChanged = false;
            PlayerData.cameraController.SetDirection(direction);
        }
    }

    public void SetDirection(int direction)
    {
        this.direction = direction;
        directionChanged = true;
    }

    private void TurnCamera()
    {
        PlayerData.cameraController.Update();

        if (Input.GetKeyDown(KeyCode.A) ||
        Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction = -1;
            directionChanged = true;
        }

        if (Input.GetKeyDown(KeyCode.D) ||
            Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction = 1;
            directionChanged = true;
        }

        if (Input.GetKeyDown(KeyCode.S) ||
            Input.GetKeyDown(KeyCode.DownArrow))
        {
            direction = 0;
            directionChanged = true;
        }
    }

    private void Interact()
    {
        GameObject navigationTrigger = GetRaycasted();

        if (navigationTrigger == null ||
            !navigationTrigger.TryGetComponent(out Raycastable raycastable))
            return;

        raycastable.OnHover();

        if (!Input.GetMouseButtonDown(0))
            return;

        raycastable.Interact();
    }

    private void ChangeMouseState()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            MouseController.SetMouseState(MouseEmotion.Calm);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            MouseController.SetMouseState(MouseEmotion.Grumpy);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            MouseController.SetMouseState(MouseEmotion.Murdery);
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            MouseController.SetMouseState(testState);
    }

    private GameObject GetRaycasted()
    {
        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out hit))
            return null;

        return hit.transform.gameObject;
    }

    private PlayerData PlayerData => PlayerData.Instance;

    private MouseController MouseController => MouseController.Instance;
}
