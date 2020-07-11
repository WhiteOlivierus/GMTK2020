using TMPro;
using UnityEditor;
using UnityEngine;

public class NavigationPoint : MonoBehaviour
{
    [SerializeField] private TextMeshPro displayText;

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
    private void OnDrawGizmos() => Handles.Label(transform.position, ParentName);
#endif
}
