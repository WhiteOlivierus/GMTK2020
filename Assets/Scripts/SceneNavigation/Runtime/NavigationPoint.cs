using TMPro;
using UnityEditor;
using UnityEngine;

public class NavigationPoint : MonoBehaviour
{
    [SerializeField] private TextMeshPro displayText = default;

    private bool isHovering = false;

    private string ParentName => transform.parent.gameObject.name;

    private void Update()
    {
        displayText.text = string.Empty;

        if (!isHovering)
            return;

        displayText.text = ParentName;

        isHovering = false;
    }

    public void OnHover() => isHovering = true;

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
}
