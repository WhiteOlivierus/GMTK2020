using UnityEditor;
using UnityEngine;

public class NavigationTrigger : Raycastable
{
    private string ParentName => transform.parent.GetChild(0).gameObject.name;

    public override void OnHover() => base.OnHover();

    public override void Interact() => SceneNavigation.Navigate(gameObject);

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Color red = Color.red;
        red.a = 0.5f;
        Gizmos.color = red;
        Gizmos.DrawCube(transform.position, transform.localScale);
        Handles.color = Color.blue;
        Handles.Label(transform.position, ParentName);
    }
#endif

    private SceneNavigation SceneNavigation => SceneNavigation.Instance;
    private PlayerData PlayerData => PlayerData.Instance;
}
