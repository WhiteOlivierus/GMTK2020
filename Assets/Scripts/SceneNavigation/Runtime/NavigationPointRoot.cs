using UnityEditor;
using UnityEngine;

public class NavigationPointRoot : MonoBehaviour
{
    public bool canTurn = false;

#if UNITY_EDITOR
    [ShowIf(nameof(canTurn), true, ShowIfComparisonType.Equals)]
#endif
    public bool fullCircle = true;

#if UNITY_EDITOR
    [ShowIf(nameof(fullCircle), false, ShowIfComparisonType.Equals)]
#endif
    [Range(0, -180)]
    public int maxAngleLeft = 0;
#if UNITY_EDITOR
    [ShowIf(nameof(fullCircle), false, ShowIfComparisonType.Equals)]
#endif
    [Range(0, 180)]
    public int maxAngleRight = 0;

#if UNITY_EDITOR
    private void OnDrawGizmos() => Handles.Label(transform.position, gameObject.name);
#endif
}
