using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class EventTrigger : Raycastable
{
    [SerializeField] private UnityEvent unityEvent = default;

    public override void Interact() => unityEvent.Invoke();

    public override void OnHover() => base.OnHover();

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Color green = Color.green;
        green.a = 0.5f;
        Gizmos.color = green;
        Gizmos.DrawCube(transform.position, transform.localScale);
        Handles.color = Color.green;
        Handles.Label(transform.position, gameObject.name);
    }
#endif
}
