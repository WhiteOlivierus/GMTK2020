using UnityEditor;
using UnityEngine;

public class NavigationPointRoot : MonoBehaviour
{
    public bool canTurn = false;

    [ShowIf(nameof(canTurn), true, ShowIfComparisonType.Equals)]
    public bool fullCircle = true;

    [ShowIf(nameof(fullCircle), false, ShowIfComparisonType.Equals)]
    [Range(0, -180)]
    public int maxAngleLeft = 0;
    [ShowIf(nameof(fullCircle), false, ShowIfComparisonType.Equals)]
    [Range(0, 180)]
    public int maxAngleRight = 0;

#if UNITY_EDITOR
    private void OnDrawGizmos() => Handles.Label(transform.position, gameObject.name);
#endif
}
