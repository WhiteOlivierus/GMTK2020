using UnityEngine;

public class TurnToCamera : MonoBehaviour
{
    private void RotateTowards()
    {
        Vector3 forward = transform.position - Camera.main.transform.position;
        transform.rotation = Quaternion.LookRotation(forward, Vector3.up);
    }

    private void OnEnable()
    {
        SceneNavigation.playerMoved += RotateTowards;
        InputManager.playerMoved += RotateTowards;
    }

    private void OnDisable()
    {
        SceneNavigation.playerMoved -= RotateTowards;
        InputManager.playerMoved -= RotateTowards;
    }

    private SceneNavigation SceneNavigation => SceneNavigation.Instance;
    private InputManager InputManager => InputManager.Instance;
}
