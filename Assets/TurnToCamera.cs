using UnityEngine;

public class TurnToCamera : MonoBehaviour
{
    private void RotateTowards()
    {
        Vector3 forward = transform.position - Camera.main.transform.position;
        transform.rotation = Quaternion.LookRotation(forward, Vector3.up);
    }

    private void Awake() => SceneNavigation.playerMoved += RotateTowards;

    private void OnDisable() => SceneNavigation.playerMoved -= RotateTowards;

    private SceneNavigation SceneNavigation => SceneNavigation.Instance;
}
